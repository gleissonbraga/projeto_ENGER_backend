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
    public class SubscriptionRepository : ISubscriptionRepository
    {
        public readonly AppDbContext _context;

        public SubscriptionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> AddAsync(Subscription subscription)
        {
            await _context.Subscriptions.AddAsync(subscription);
            await _context.SaveChangesAsync();

            return subscription.SubscriptionCode;
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Company?> GetByIdAsync(int subscriptionId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Subscription subscription)
        {
            throw new NotImplementedException();
        }
    }
}
