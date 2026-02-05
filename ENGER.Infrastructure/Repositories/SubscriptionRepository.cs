using ENGER.Domain.Entities;
using ENGER.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENGER.Infrastructure.Repositories
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        public Task AddAsync(Subscription subscription)
        {
            throw new NotImplementedException();
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
