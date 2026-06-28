using ENGER.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENGER.Domain.Interfaces.Repositories
{
    public interface IConstructionRepository
    {
        Task<Construction> AddAsync(Construction construction);
        Task<ConstructionPayment> AddPaymentAsync(ConstructionPayment payment);
        Task<Construction> UpdateAsync(Construction construction);
        Task<Construction?> GetByIdAsync(int intConstructionId, int intCompanyId);
        Task<IEnumerable<Construction>> GetByCompanyAsync(int intCompanyId);
    }
}
