using ENGER.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENGER.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User> AddAsync(User user);
        Task<User> UpdateAsync(User user);
        Task DeleteAsync(int id);
        Task<User?> GetByIdAsync(int userid, int intCompanyId);
        Task InactiveUser(User user);
        Task ActiveUser(User user);
        Task<IEnumerable<User>> GetByCompanyIdAsync(int companyId);
        Task<User> GetByEmail(string email, int companyId);
    }
}
