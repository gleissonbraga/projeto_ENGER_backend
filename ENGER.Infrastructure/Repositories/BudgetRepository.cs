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
    public class BudgetRepository : IBudgetRepository
    {
        private readonly AppDbContext _context;

        public BudgetRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task<int> AddAsync(Budget budget)
        {
            throw new NotImplementedException();
        }

        public Task<Budget?> GetByBudgetCompanyAsync(int intCompanyId)
        {
            throw new NotImplementedException();
        }

        public Task<Budget?> GetByIdAsync(int intBudgetId, int intCompanyId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Budget budget)
        {
            throw new NotImplementedException();
        }
    }
}
