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
        public DbSet<User> Users => Set<User>();
        public DbSet<Card> Cards => Set<Card>();
        public DbSet<Employee> Employees => Set<Employee>();
        public DbSet<Position> Positions => Set<Position>();
        public DbSet<Client> Clients => Set<Client>();
        public DbSet<Budget> Budgets => Set<Budget>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
