using ENGER.Domain.Entities;
using ENGER.Domain.Interfaces.Repositories;
using ENGER.Infrastructure.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENGER.Infrastructure.Repositories
{
    public class SubscriptionTypeRepository : ISubscriptionTypeRepository
    {

        private readonly AppDbContext _context;

        public SubscriptionTypeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(SubscriptionType subscriptionType)
        {
            await _context.SubscriptionTypes.AddAsync(subscriptionType);
            await _context.SaveChangesAsync();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SubscriptionType>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Company?> GetByIdAsync(int subscriptionId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(SubscriptionType subscriptionType)
        {
            throw new NotImplementedException();
        }
    }
}
