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
        Task<int> AddAsync(Company company);
        Task UpdateAsync(Company company);
        Task<Company?> GetByIdAsync(int intCompanyId);

        Task<Company?> GetByReasonNameAsync(string strName);
        Task<Company?> GetByCPFCNPJAsync(string strNumberRegistrations);
        Task<Company?> GetByAddressEmailAsync(string strEmailAddress);
        Task<Company?> GetByNumberIERGAsync(string strIERG);
    }
}
