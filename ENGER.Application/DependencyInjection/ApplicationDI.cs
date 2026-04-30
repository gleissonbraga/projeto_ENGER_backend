using ENGER.Application.UseCases.Budget.Create;
using ENGER.Application.UseCases.Budget.GetAll;
using ENGER.Application.UseCases.Budget.GetByID;
using ENGER.Application.UseCases.Card.GetByIdCompany;
using ENGER.Application.UseCases.Client.Create;
using ENGER.Application.UseCases.Client.GetAll;
using ENGER.Application.UseCases.Client.GetById;
using ENGER.Application.UseCases.Company.Create;
using ENGER.Application.UseCases.Construction.Create;
using ENGER.Application.UseCases.Employee.Active;
using ENGER.Application.UseCases.Employee.Create;
using ENGER.Application.UseCases.Employee.GetAll;
using ENGER.Application.UseCases.Employee.GetById;
using ENGER.Application.UseCases.Employee.Inactive;
using ENGER.Application.UseCases.Employee.Update;
using ENGER.Application.UseCases.Position.Create;
using ENGER.Application.UseCases.Subscription.Create;
using ENGER.Application.UseCases.SubscriptionType.Create;
using ENGER.Application.UseCases.User.Create;
using ENGER.Application.UseCases.User.Delete;
using ENGER.Application.UseCases.User.GetAll;
using ENGER.Application.UseCases.User.GetById;
using ENGER.Application.UseCases.User.GetUserById;
using ENGER.Application.UseCases.User.Update;
using ENGER.Application.UseCases.User.UpdateUser;
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
            // Company
            services.AddScoped<UpdatePaymentUseCase>();

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

            // Subscription
            services.AddScoped<CreateSubscriptionUseCase>();

            // Payment
            services.AddScoped<UpdatePaymentUseCase>();

            // Position
            services.AddScoped<CreatePositionUseCase>();
            services.AddScoped<UpdatePositionUseCase>();
            services.AddScoped<GetByIdPositionUseCase>();
            services.AddScoped<GetAllPositionByCompanyUseCase>();

            // Employee
            services.AddScoped<CreateEmployeeUseCase>();
            services.AddScoped<UpdateEmployeeUseCase>();
            services.AddScoped<GetByIdEmployeeUseCase>();
            services.AddScoped<GetAllEmployeesByCompanyUseCase>();
            services.AddScoped<ActiveEmployeeUseCase>();
            services.AddScoped<InactiveEmployeeUseCase>();

            // Client
            services.AddScoped<CreateClientUseCase>();
            services.AddScoped<UpdateClientUseCase>();
            services.AddScoped<GetByIdClientUseCase>();
            services.AddScoped<GetAllClientsByCompanyUseCase>();

            // Budget
            services.AddScoped<CreateBudgetUseCase>();
            services.AddScoped<GetByIdBudgetUseCase>();
            services.AddScoped<GetAllBudgetUseCase>();

            // Construction
            services.AddScoped<CreateConstructionUseCase>();

            return services;
        }
    }
}
