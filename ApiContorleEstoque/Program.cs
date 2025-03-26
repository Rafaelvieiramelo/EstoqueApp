using LidyDecorApp.Application.Interfaces;
using LidyDecorApp.Application.Mapping;
using LidyDecorApp.Application.Services;
using LidyDecorApp.Domain.Interfaces;
using LidyDecorApp.Infrastructure;
using LidyDecorApp.Infrastructure.Filters;
using LidyDecorApp.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Configura��o do CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

// Configura��o dos controllers e JSON
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
});

// Configura��o da autentica��o JWT
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

// Configura��o de autoriza��o com roles
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
    options.AddPolicy("User", policy => policy.RequireRole("User"));

    options.AddPolicy("AcessoProdutosClientes", policy => policy.RequireRole("Admin", "User")); // Admin e User acessam
    options.AddPolicy("AcessoTotal", policy => policy.RequireRole("Admin")); // S� Admin acessa
});



// Inje��o de depend�ncias para os servi�os e reposit�rios
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

// Configura��o do banco de dados
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<LidyDecorDbContext>(options =>
    options.UseSqlite(connectionString));

var app = builder.Build();

// Configura��o do middleware
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseCors("AllowAll");

app.MapControllers();

await app.RunAsync();
