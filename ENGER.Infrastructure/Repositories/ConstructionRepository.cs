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
    public class ConstructionRepository : IConstructionRepository
    {

        private readonly AppDbContext _context;

        public ConstructionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Construction> AddAsync(Construction construction)
        {
            await _context.AddAsync(construction);
            await _context.SaveChangesAsync();

            return construction;
        }

        public Task<IEnumerable<Construction>> GetByCompanyAsync(int intCompanyId)
        {
            throw new NotImplementedException();
        }

        public Task<Construction?> GetByIdAsync(int intConstructionId, int intCompanyId)
        {
            throw new NotImplementedException();
        }

        public async Task<Construction> UpdateAsync(Construction construction)
        {
            _context.Update(construction);
            await _context.SaveChangesAsync();

            return construction;
        }
    }
}
