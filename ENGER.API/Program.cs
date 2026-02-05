using DotNetEnv;
using ENGER.Application.DependencyInjection;
using ENGER.Infrastructure.Data.Context;
using ENGER.Infrastructure.DependencyInjection; // Importante para enxergar o método de extensão
using Microsoft.EntityFrameworkCore;


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