using ENGER.Domain.Entities;
using ENGER.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENGER.Infrastructure.Repositories
{
    public class PositionRepository : IPositionRepository
    {
        public Task<int> AddAsync(Position position)
        {
            throw new NotImplementedException();
        }

        public Task<Position?> DeleteAsync(int intPositionId)
        {
            throw new NotImplementedException();
        }

        public Task<Position?> GetByIdAsync(int intPositionId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Position position)
        {
            throw new NotImplementedException();
        }
    }
}
