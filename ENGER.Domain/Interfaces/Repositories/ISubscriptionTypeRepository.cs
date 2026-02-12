using ENGER.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENGER.Domain.Interfaces.Repositories
{
    public interface ISubscriptionTypeRepository
    {
        Task<SubscriptionType> AddAsync(SubscriptionType subscriptionType);
        Task<SubscriptionType> UpdateAsync(SubscriptionType subscriptionType);
        Task DeleteAsync(SubscriptionType subscriptionType);
        Task<SubscriptionType?> GetByIdAsync(int subscriptionId);
        Task<IEnumerable<SubscriptionType>> GetAllAsync();
    }
}
