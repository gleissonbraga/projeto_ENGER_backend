using ENGER.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ENGER.Infrastructure.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
       : base(options) { }

        public DbSet<Company> Companies => Set<Company>();
        public DbSet<Subscription> Subscriptions => Set<Subscription>();
        public DbSet<SubscriptionType> SubscriptionTypes => Set<SubscriptionType>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Esta linha varre o seu projeto Infrastructure em busca de todas as classes 
            // que herdam de IEntityTypeConfiguration (como o seu CompanyMap) e as aplica.
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
