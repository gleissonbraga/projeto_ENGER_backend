using ENGER.Domain.Interfaces.Repositories;
using ENGER.Infrastructure.Data.Context;
using ENGER.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENGER.Infrastructure.DependencyInjection
{
    public static class InfrastructureDI
    {
        public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("Default")));

            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<ISubscriptionTypeRepository, SubscriptionTypeRepository>();
            services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICardRepository, CardRepository>();

            return services;
        }
    }
}
