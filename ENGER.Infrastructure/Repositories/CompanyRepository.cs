using ENGER.Domain.Entities;
using ENGER.Domain.Interfaces.Repositories;
using ENGER.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ENGER.Infrastructure.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly AppDbContext _context;

        public CompanyRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<int> AddAsync(Company company)
        {
            await _context.Companies.AddAsync(company);
            await _context.SaveChangesAsync();

            return company.CompanyId;
        }

        public async Task<Company?> GetByAddressEmailAsync(string strEmailAddress)
        {
            Company objCompany = await _context.Companies.FirstOrDefaultAsync(x => x.Email == strEmailAddress);
            return objCompany;
        }

        public async Task<Company?> GetByCPFCNPJAsync(string strNumberRegistrations)
        {
            Company objCompany = await _context.Companies.FirstOrDefaultAsync(x => x.RegistrationNumber == strNumberRegistrations);
            return objCompany;
        }

        public async Task<Company?> GetByIdAsync(int codigoEmpresa)
        {
            Company objCompany = await _context.Companies.FindAsync(codigoEmpresa);
            return objCompany;
        }

        public async Task<Company?> GetByNumberIERGAsync(string strIERG)
        {
            Company objCompany = await _context.Companies.FirstOrDefaultAsync(x => x.RGIENumber == strIERG);
            return objCompany;
        }

        public async Task<Company?> GetByReasonNameAsync(string name)
        {
            Company objCompany = await _context.Companies.FirstOrDefaultAsync(x => x.ReasonName == name);
            return objCompany;
        }

        public Task UpdateAsync(Company company)
        {
            throw new NotImplementedException();
        }

    }
}
