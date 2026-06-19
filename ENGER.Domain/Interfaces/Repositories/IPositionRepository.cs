using ENGER.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENGER.Domain.Interfaces.Repositories
{
    public interface IPositionRepository
    {
        Task<Position> AddAsync(Position position);
        Task UpdateAsync(Position position);
        Task<Position?> GetByIdAsync(int intPositionId, int companyId);
        Task DeleteAsync(Position position);
        Task<IEnumerable<Position>> GetAllPositions(int companyId);
    }
}
