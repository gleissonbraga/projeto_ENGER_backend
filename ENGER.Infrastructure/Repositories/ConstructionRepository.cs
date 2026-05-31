using ENGER.Domain.Entities;
using ENGER.Domain.Interfaces.Repositories;
using ENGER.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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

        public async Task<IEnumerable<Construction>> GetByCompanyAsync(int intCompanyId)
        {
            return await _context.Constructions
                    .Include(x => x.Stages)
                    .Include(x => x.Attachments)
                    .Include(x => x.Presences)
                    .Include(x => x.Rentals)
                    .Include(x => x.Employees)
                    .Where(x => x.CompanyId == intCompanyId)
                    .ToListAsync();
        }

        public async Task<Construction?> GetByIdAsync(int intConstructionId, int intCompanyId)
        {
            return await _context.Constructions.FirstOrDefaultAsync(x => x.ConstructionId == intConstructionId && x.CompanyId == intCompanyId);
        }

        public async Task<Construction> UpdateAsync(Construction construction)
        {
            _context.Update(construction);
            await _context.SaveChangesAsync();

            return construction;
        }
    }
}
