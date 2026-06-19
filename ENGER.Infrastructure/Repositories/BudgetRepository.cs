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
    public class BudgetRepository : IBudgetRepository
    {
        private readonly AppDbContext _context;

        public BudgetRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Budget> AddAsync(Budget budget)
        {
            await _context.Budgets.AddAsync(budget);
            await _context.SaveChangesAsync();

            return budget;
        }

        public async Task<IEnumerable<Budget>> GetByBudgetCompanyAsync(int intCompanyId)
        {
            IEnumerable<Budget> objBudgets = await _context.Budgets
                        .Include(x => x.Client)
                            .Include(x => x.Stages)
                                .ThenInclude(s => s.Materials)
                            .Include(x => x.Stages)
                                .ThenInclude(s => s.Labors)
                        .Where(x => x.CompanyId == intCompanyId).ToListAsync();

            return objBudgets;
        }

        public async Task<Budget?> GetByIdAsync(int intBudgetId, int intCompanyId)
        {
            Budget objBudget = await _context.Budgets.Include(x => x.Client)
                            .Include(x => x.Stages)
                                .ThenInclude(s => s.Materials)
                            .Include(x => x.Stages)
                                .ThenInclude(s => s.Labors)
                             .FirstOrDefaultAsync(x => x.CompanyId == intCompanyId && x.BudgetId == intBudgetId);

            return objBudget;
        }

        public async Task<Budget?> GetByKeyAsync(Guid keyBudget, int intCompanyId)
        {
            Budget objBudget = await _context.Budgets.Include(x => x.Client)
                .Include(x => x.Stages)
                    .ThenInclude(s => s.Materials)
                .Include(x => x.Stages)
                    .ThenInclude(s => s.Labors)
                 .FirstOrDefaultAsync(x => x.CompanyId == intCompanyId && x.KeyBudget == keyBudget);

            return objBudget;
        }

        public async Task<Budget?> UpdateAsync(Budget budget)
        {
            _context.Budgets.Update(budget);
            await _context.SaveChangesAsync();

            return budget;
        }
    }
}
