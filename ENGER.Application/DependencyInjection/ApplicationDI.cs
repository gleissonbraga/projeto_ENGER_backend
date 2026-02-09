using ENGER.Application.UseCases.Company.Create;
using ENGER.Application.UseCases.SubscriptionType.Create;
using ENGER.Application.UseCases.User.GetAll;
using ENGER.Domain.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENGER.Application.DependencyInjection
{
    public static class ApplicationDI
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Registramos os UseCases aqui
            // Usamos Scoped para que o UseCase viva durante o tempo da requisição HTTP
            services.AddScoped<CreateCompanyUseCase>();
            services.AddScoped<CreateSubscriptionTypeUsecase>();
            services.AddScoped<CreateUsersUseCase>();

            // Se você tiver outros, adiciona aqui embaixo:
            // services.AddScoped<GetCompanyByIdUseCase>();

            return services;
        }
    }
}
