using ENGER.Infrastructure.DependencyInjection; // Importante para enxergar o método de extensão
using ENGER.Application.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

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