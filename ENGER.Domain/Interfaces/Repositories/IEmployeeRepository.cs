using ENGER.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENGER.Domain.Interfaces.Repositories
{
    public interface IEmployeeRepository
    {
        Task<Employee> AddAsync(Employee employee);
        Task<Employee> UpdateAsync(Employee employee);
        Task DeleteAsync(int id);
        Task<Employee?> GetByIdAsync(int employeeId, int intCompanyId);
        Task InactiveEmployee(Employee employee);
        Task ActiveEmployee(Employee employee);
        Task<IEnumerable<Employee>> GetByCompanyIdAsync(int companyId);
        Task<bool> AnyEmployeeWithEmailAsync(int companyId, string email, int? employeeId);
        Task<bool> AnyEmployeeWithNumberRegistrationAsync(int companyId, string registrationNumber, int? employeeId);
    }
}
