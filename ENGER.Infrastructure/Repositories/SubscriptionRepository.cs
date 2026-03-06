using ENGER.Domain.Entities;
using ENGER.Domain.Interfaces.Repositories;
using ENGER.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
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

        public async Task<int> AddAsync(Subscription subscription)
        {
            await _context.Subscriptions.AddAsync(subscription);
            await _context.SaveChangesAsync();

            return subscription.SubscriptionId;
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Subscription?> GetByIdAsync(int subscriptionId)
        {
            Subscription objSubscription = await _context.Subscriptions.FindAsync(subscriptionId);
            return objSubscription;
        }

        public async Task<Subscription?> GetBySubscriptionKeyAccess(Guid subscriptionId)
        {
            Subscription objSubscription = await _context.Subscriptions.FirstOrDefaultAsync(x => x.SubscriptionCode == subscriptionId);
            return objSubscription;
        }

        public async Task UpdateAsync(Subscription subscription)
        {
            _context.Subscriptions.Update(subscription);
            await _context.SaveChangesAsync();
        }
    }
}
