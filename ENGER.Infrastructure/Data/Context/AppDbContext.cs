using Microsoft.EntityFrameworkCore;
using ENGER.Domain.Entities;

namespace ENGER.Infrastructure.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
       : base(options) { }


        public DbSet<Company> Companies => Set<Company>();
    }
}
