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
        Task AddAsync(SubscriptionType subscriptionType);
        Task UpdateAsync(SubscriptionType subscriptionType);
        Task DeleteAsync(int id);
        Task<Company?> GetByIdAsync(int subscriptionId);
        Task<IEnumerable<SubscriptionType>> GetAllAsync();
    }
}
