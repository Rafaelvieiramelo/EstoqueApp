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

builder.Services.AddAutoMapper(typeof(MappingProfile));

// Configuração do banco de dados
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<LidyDecorDbContext>(options => options.UseSqlite(connectionString));

builder.Services.AddValidatorsFromAssemblyContaining<ClientesDTOValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<OrcamentoDTOValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<ProdutosDTOValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UsuarioWriteDTOValidator>();

var app = builder.Build();

// Configuração do middleware
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseCors("AllowAll");

app.MapControllers();

await app.RunAsync();
