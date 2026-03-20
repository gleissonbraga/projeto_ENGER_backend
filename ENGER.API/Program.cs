using DotNetEnv;
using ENGER.Application.DependencyInjection;
using ENGER.Infrastructure.Data.Context;
using ENGER.Infrastructure.DependencyInjection; // Importante para enxergar o método de extensão
using Microsoft.EntityFrameworkCore;
using MercadoPago.Config;

//MercadoPagoConfig.AccessToken = "TEST-8390326417261248-030317-f61af5e647880f935dc6f37c4d846867-2685085537";


var builder = WebApplication.CreateBuilder(args);

Env.Load("../postgres.env");
var connectionString = $"Host={Environment.GetEnvironmentVariable("DB_HOST")};" +
                       $"Port={Environment.GetEnvironmentVariable("DB_PORT")};" +
                       $"Database={Environment.GetEnvironmentVariable("DB_NAME")};" +
                       $"Username={Environment.GetEnvironmentVariable("DB_USER")};" +
                       $"Password={Environment.GetEnvironmentVariable("DB_PASS")};";

builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString));

// 1. Configuração do Banco e Repositórios (Infra)
// O builder.Configuration busca automaticamente as chaves do appsettings.json
builder.Services.AddInfrastructure(builder.Configuration);

// 2. Configuração dos UseCases (Application)
builder.Services.AddApplication();


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configuração do Pipeline de Requisições (Middleware)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();