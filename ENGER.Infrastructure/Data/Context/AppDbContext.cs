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
        public DbSet<BudgetLabor> BudgetLabors => Set<BudgetLabor>();
        public DbSet<BudgetMaterial> BudgetMaterial => Set<BudgetMaterial>();
        public DbSet<BudgetStage> BudgetStages => Set<BudgetStage>();
        public DbSet<Construction> Constructions => Set<Construction>();
        public DbSet<ConstructionAttachment> ConstructionsAttachments => Set<ConstructionAttachment>();
        public DbSet<ConstructionEmployee> ConstructionsEmployees => Set<ConstructionEmployee>();
        public DbSet<ConstructionPayment> ConstructionPayments => Set<ConstructionPayment>();
        public DbSet<ConstructionPresence> ConstructionPresences => Set<ConstructionPresence>();
        public DbSet<ConstructionRental> ConstructionRentals => Set<ConstructionRental>();
        public DbSet<ConstructionStage> ConstructionStages => Set<ConstructionStage>();
        public DbSet<SendEmail> SendEmails => Set<SendEmail>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
