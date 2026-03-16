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
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task ActiveEmployee(Employee employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }

        public async Task<Employee> AddAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            return employee;
        }

        public async Task<bool> AnyEmployeeWithEmailAsync(int companyId, string email, int? employeeId)
        {
            if(employeeId.HasValue)
                return await _context.Employees
                                .AnyAsync(x => x.CompanyId == companyId &&
                                               x.Email == email &&
                                               x.EmployeeId != employeeId);
            else
                return await _context.Employees
                                .AnyAsync(x => x.CompanyId == companyId &&
                                               x.Email == email);
        }

        public async Task<bool> AnyEmployeeWithNumberRegistrationAsync(int companyId, string registrationNumber, int? employeeId)
        {
            if (employeeId.HasValue)
                return await _context.Employees
                                .AnyAsync(x => x.CompanyId == companyId &&
                                               x.RegistrationNumber == registrationNumber &&
                                               x.EmployeeId != employeeId);
            else
                return await _context.Employees
                                .AnyAsync(x => x.CompanyId == companyId &&
                                               x.RegistrationNumber == registrationNumber);
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Employee>> GetByCompanyIdAsync(int companyId)
        {
            return await _context.Employees.Where(x => x.CompanyId == companyId).ToListAsync();
        }

        public async Task<Employee?> GetByIdAsync(int employeeId, int intCompanyId)
        {
            return await _context.Employees.FirstOrDefaultAsync(x => x.EmployeeId == employeeId && x.CompanyId == intCompanyId);
        }

        public async Task InactiveEmployee(Employee employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }

        public async Task<Employee> UpdateAsync(Employee employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();

            return employee;
        }
    }
}
