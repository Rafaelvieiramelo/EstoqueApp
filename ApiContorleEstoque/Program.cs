using FluentValidation;
using LidyDecorApp.Application.Interfaces;
using LidyDecorApp.Application.Mapping;
using LidyDecorApp.Application.Services;
using LidyDecorApp.Application.Validators;
using LidyDecorApp.Domain.Interfaces;
using LidyDecorApp.Infrastructure;
using LidyDecorApp.Infrastructure.Filters;
using LidyDecorApp.Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configuração do CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

// Configuração dos controllers e JSON
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.MaxDepth = 64;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SchemaFilter<SwaggerExcludeFilter>();

    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Sua API", Version = "v1" });

    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Insira o token JWT no formato: Bearer {seu token}",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        }
    };

    options.AddSecurityDefinition("Bearer", securityScheme);

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            securityScheme,
            Array.Empty<string>()
        }
    });
});

// Configuração da autenticação JWT
var jwtKey = builder.Configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key is not configured.");
var jwtIssuer = builder.Configuration["Jwt:Issuer"];
var jwtAudience = builder.Configuration["Jwt:Audience"];

var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = key
        };
    });

// Configuração de autorização com roles
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
    options.AddPolicy("User", policy => policy.RequireRole("User"));

    options.AddPolicy("AcessoProdutosClientes", policy => policy.RequireRole("Admin", "User")); // Admin e User acessam
    options.AddPolicy("AcessoTotal", policy => policy.RequireRole("Admin")); // Só Admin acessa
});



// Injeção de dependências para os serviços e repositórios
builder.Services.AddScoped<IProdutosService, ProdutosService>();
builder.Services.AddScoped<IUsuariosService, UsuariosService>();
builder.Services.AddScoped<IClientesService, ClientesService>();
builder.Services.AddScoped<IOrcamentosService, OrcamentosService>();

builder.Services.AddScoped<IProdutosRepository, ProdutosRepository>();
builder.Services.AddScoped<IUsuariosRepository, UsuariosRepository>();
builder.Services.AddScoped<IClientesRepository, ClientesRepository>();
builder.Services.AddScoped<IOrcamentosRepository, OrcamentosRepository>();

builder.Services.AddTransient<IJwtTokenService, JwtTokenService>();
builder.Services.AddScoped<LidyDecorApp.Application.Interfaces.IContratoService, LidyDecorApp.Infrastructure.Services.ContratoService>();

builder.Services.AddAutoMapper(typeof(MappingProfile));

// Configuração do banco de dados
var rawConnectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
                           ?? Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection")
                           ?? Environment.GetEnvironmentVariable("DATABASE_URL");

var connectionString = ConvertPostgresUrlToConnectionString(rawConnectionString);

builder.Services.AddDbContext<LidyDecorDbContext>(options => 
    options.UseNpgsql(connectionString));

// Health Checks
builder.Services.AddHealthChecks()
    .AddDbContextCheck<LidyDecorDbContext>();

builder.Services.AddValidatorsFromAssemblyContaining<ClientesDTOValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<OrcamentoDTOValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<ProdutosDTOValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UsuarioWriteDTOValidator>();

var app = builder.Build();

// Execução de migrações e seed de dados
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();
    try
    {
        var context = services.GetRequiredService<LidyDecorDbContext>();
        logger.LogInformation("Verificando conexão com o banco de dados PostgreSQL...");
        
        // Em produção ou se solicitado via variável de ambiente, aplica as migrações automaticamente
        if (app.Environment.IsProduction() || Environment.GetEnvironmentVariable("RUN_MIGRATIONS") == "true")
        {
            logger.LogInformation("Aplicando migrações pendentes em Produção...");
            await context.Database.MigrateAsync();
        }
        
        await LidyDecorApp.Infrastructure.Services.DbSeeder.SeedAsync(context);
        logger.LogInformation("Banco de dados verificado e semeado com sucesso.");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Erro no tratamento de conexão/migração inicial do PostgreSQL.");
    }
}

// Configuração do middleware
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseCors("AllowAll");

app.MapControllers();
app.MapHealthChecks("/health");

var port = Environment.GetEnvironmentVariable("PORT");
if (!string.IsNullOrEmpty(port))
{
    app.Urls.Add($"http://*:{port}");
}

await app.RunAsync();

// Local helper function to convert postgres:// url to Npgsql connection string
string ConvertPostgresUrlToConnectionString(string? url)
{
    if (string.IsNullOrWhiteSpace(url)) return string.Empty;
    if (!url.StartsWith("postgres://", StringComparison.OrdinalIgnoreCase)) return url;

    try
    {
        var uri = new Uri(url);
        var userInfo = uri.UserInfo.Split(':');
        var username = userInfo[0];
        var password = userInfo.Length > 1 ? userInfo[1] : "";
        var host = uri.Host;
        var port = uri.Port > 0 ? uri.Port : 5432;
        var database = uri.AbsolutePath.TrimStart('/');

        return $"Host={host};Port={port};Database={database};Username={username};Password={password};SSL Mode=Require;Trust Server Certificate=true;";
    }
    catch
    {
        return url;
    }
}
