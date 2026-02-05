using ENGER.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENGER.Domain.Interfaces.Repositories
{
    public interface ICompanyRepository
    {
        Task AddAsync(Company company);
        Task UpdateAsync(Company company);
        Task<Company?> GetByIdAsync(int companyId);
    }
}
