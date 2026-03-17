using ENGER.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENGER.Domain.Interfaces.Repositories
{
    public interface IBudgetRepository
    {
        Task<int> AddAsync(Budget budget);
        Task UpdateAsync(Budget budget);
        Task<Budget?> GetByIdAsync(int intBudgetId, int intCompanyId);
        Task<Budget?> GetByBudgetCompanyAsync(int intCompanyId);
    }
}
