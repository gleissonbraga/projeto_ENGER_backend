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
    public class SubscriptionTypeRepository : ISubscriptionTypeRepository
    {

        private readonly AppDbContext _context;

        public SubscriptionTypeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task DeleteAsync(SubscriptionType subscriptionType)
        {
            _context.SubscriptionTypes.Remove(subscriptionType);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<SubscriptionType>> GetAllAsync()
        {
            IEnumerable<SubscriptionType> subsTypes = await _context.SubscriptionTypes.ToListAsync();

            return subsTypes;
        }

        public async Task<SubscriptionType> AddAsync(SubscriptionType subscriptionType)
        {
            await _context.SubscriptionTypes.AddAsync(subscriptionType);
            await _context.SaveChangesAsync();

            return subscriptionType;
        }

        public async Task<SubscriptionType?> GetByIdAsync(int subscriptionId)
        {
            SubscriptionType subTypes = await _context.SubscriptionTypes.FindAsync(subscriptionId);
            await _context.SaveChangesAsync();

            return subTypes;
        }

        public async Task<SubscriptionType> UpdateAsync(SubscriptionType subscriptionType)
        {
            _context.SubscriptionTypes.Update(subscriptionType);
            await _context.SaveChangesAsync();

            return subscriptionType;
        }
    }
}
