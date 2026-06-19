using DotNetEnv;
using ENGER.Application.DependencyInjection;
using ENGER.Domain.Services;
using ENGER.Infrastructure.Data.Context;
using ENGER.Infrastructure.DependencyInjection;
using ENGER.Infrastructure.Security;
using ENGER.Infrastructure.Services;
using MercadoPago.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer; // 🚀 ADICIONADO
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens; // 🚀 ADICIONADO
using Resend;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Carrega os ambientes ANTES de tentar ler a JWT_KEY
Env.Load("../postgres.env");
Env.Load("../environment.env");

builder.Services.AddCors(options =>
{
    options.AddPolicy("EngerPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:3000", "https://localhost:3000", "http://10.0.0.128:3000", "https://enger.vercel.app/")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

builder.Services.AddHttpClient();

// 🚀 [ADICIONADO AQUI] - Configuração para o .NET saber ler e validar o JWT do Cookie HttpOnly
var secretKey = Environment.GetEnvironmentVariable("JWT_KEY") ?? throw new Exception("JWT_KEY não configurada nas variáveis de ambiente.");

var key = Encoding.ASCII.GetBytes(secretKey); JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true
    };

    // 🍪 ESSENCIAL: Diz para o .NET pegar o Token de dentro do seu Cookie Customizado
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            if (context.Request.Cookies.ContainsKey("EngerAuthToken"))
            {
                context.Token = context.Request.Cookies["EngerAuthToken"];
            }
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddAuthorization(options =>
{
    // Política 1: Só entra quem tiver a claim "AdminLevel" igual a "7" (Master)
    options.AddPolicy("RequerNivelMaster", policy =>
        policy.RequireClaim("AdminLevel", "7"));

    // Política 2: Exemplo para quem é Gestor ou Master (Níveis 5, 6 ou 7)
    options.AddPolicy("RequerNivelGestor", policy =>
        policy.RequireClaim("AdminLevel", "5", "6", "7"));
});

var connectionString = $"Host={Environment.GetEnvironmentVariable("DB_HOST")};" +
                       $"Port={Environment.GetEnvironmentVariable("DB_PORT")};" +
                       $"Database={Environment.GetEnvironmentVariable("DB_NAME")};" +
                       $"Username={Environment.GetEnvironmentVariable("DB_USER")};" +
                       $"Password={Environment.GetEnvironmentVariable("DB_PASS")};";

builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString));

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<ResendClientOptions>(options =>
{
    options.ApiToken = "re_QGSZEvx2_B7pnHr2EJE4zTbsSYaTT28s7";
});

builder.Services.AddTransient<IResend, ResendClient>();
builder.Services.AddHostedService<EmailWorkerService>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserContext, UserContext>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("EngerPolicy");

// 🚀 [ADICIONADO AQUI] - A ordem importa! Autenticação vem ANTES da Autorização
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();