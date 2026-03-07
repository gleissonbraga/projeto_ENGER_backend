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
        Task<int> AddAsync(Position position);
        Task UpdateAsync(Position position);
        Task<Position?> GetByIdAsync(int intPositionId);
        Task<Position?> DeleteAsync(int intPositionId);
    }
}
