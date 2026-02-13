using ENGER.Application.UseCases.Company.Create;
using ENGER.Application.UseCases.SubscriptionType.Create;
using ENGER.Application.UseCases.User.GetAll;
using ENGER.Application.UseCases.User.Create;
using ENGER.Domain.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENGER.Application.UseCases.User.GetUserById;
using ENGER.Application.UseCases.User.UpdateUser;
using ENGER.Application.UseCases.User.Update;
using ENGER.Application.UseCases.User.Delete;
using ENGER.Application.UseCases.User.GetById;
using ENGER.Application.UseCases.Card.GetByIdCompany;

namespace ENGER.Application.DependencyInjection
{
    public static class ApplicationDI
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Company
            services.AddScoped<CreateCompanyUseCase>();

            // Users
            services.AddScoped<CreateUsersUseCase>();
            services.AddScoped<GetAllUsersUseCase>();
            services.AddScoped<GetUserByIdUseCase>();
            services.AddScoped<UpdateUserUseCase>();

            // Subscription Type
            services.AddScoped<CreateSubscriptionTypeUsecase>();
            services.AddScoped<UpdateTypeUseCase>();
            services.AddScoped<DeleteTypeUseCase>();
            services.AddScoped<GetAllTypesUseCase>();
            services.AddScoped<GetByIdTypeUseCase>();

            // Card
            services.AddScoped<GetCardUseCase>();

            return services;
        }
    }
}
