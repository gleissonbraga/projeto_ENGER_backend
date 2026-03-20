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
    public class PositionRepository : IPositionRepository
    {
        private readonly AppDbContext _context;

        public PositionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Position> AddAsync(Position position)
        {
            await _context.Positions.AddAsync(position);
            await _context.SaveChangesAsync();

            return position;
        }

        public async Task DeleteAsync(Position position)
        {
            _context.Positions.Remove(position);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Position>> GetAllPositions(int companyId)
        {
            return await _context.Positions.Where(x => x.CompanyId == companyId).ToListAsync();
        }

        public async Task<Position?> GetByIdAsync(int intPositionId, int companyId)
        {
            Position objPosition = await _context.Positions.FirstOrDefaultAsync(x => x.CompanyId == companyId && x.PositionId == intPositionId);

            return objPosition;
        }

        public async Task UpdateAsync(Position position)
        {
            _context.Positions.Update(position);

            await _context.SaveChangesAsync();
        }
    }
}
