using ENGER.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENGER.Domain.Interfaces.Repositories
{
    public interface IClientRepository
    {
        Task<Client> AddAsync(Client client);
        Task<Client> UpdateAsync(Client client);
        Task DeleteAsync(int id);
        Task<Client?> GetByIdAsync(int clientId, int intCompanyId);
        Task<IEnumerable<Client>> GetByCompanyIdAsync(int companyId);
        Task<Client?> GetByReasonNameAsync(string strName, int intCompanyId);
        Task<Client?> GetByCPFCNPJAsync(string strNumberRegistrations, int intCompanyId);
        Task<Client?> GetByAddressEmailAsync(string strEmailAddress, int intCompanyId);
        Task<Client?> GetByNumberIERGAsync(string strIERG, int intCompanyId);
        Task<bool> AnyClientWithEmailAsync(int companyId, string email, int? employeeId);
        Task<bool> AnyClientWithNumberRegistrationAsync(int companyId, string registrationNumber, int? employeeId);
    }
}
