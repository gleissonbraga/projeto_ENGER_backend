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
    public class ClientRepository : IClientRepository
    {
        private readonly AppDbContext _context;

        public ClientRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Client> AddAsync(Client client)
        {
            await _context.Clients.AddAsync(client);
            await _context.SaveChangesAsync();

            return client;
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Client?> GetByAddressEmailAsync(string strEmailAddress, int intCompanyId)
        {
            Client objClient = await _context.Clients.FirstOrDefaultAsync(x => x.Email == strEmailAddress && x.CompanyId == intCompanyId);
            return objClient;
        }

        public async Task<IEnumerable<Client>> GetByCompanyIdAsync(int companyId)
        {
            IEnumerable<Client> objClients = await _context.Clients.Where(x => x.CompanyId == companyId).ToListAsync();
            return objClients;
        }

        public async Task<Client?> GetByCPFCNPJAsync(string strNumberRegistrations, int intCompanyId)
        {
            Client objClient = await _context.Clients.FirstOrDefaultAsync(x => x.RegistrationNumber == strNumberRegistrations && x.CompanyId == intCompanyId);
            return objClient;
        }

        public async Task<Client?> GetByIdAsync(int clientId, int intCompanyId)
        {
            Client objClient = await _context.Clients.FirstOrDefaultAsync(x => x.ClientId == clientId && x.CompanyId == intCompanyId);
            return objClient;
        }

        public async Task<Client?> GetByNumberIERGAsync(string strIERG, int intCompanyId)
        {
            Client objClient = await _context.Clients.FirstOrDefaultAsync(x => x.RGIENumber == strIERG && x.CompanyId == intCompanyId);
            return objClient;
        }

        public async Task<Client?> GetByReasonNameAsync(string strName, int intCompanyId)
        {
            Client objClient = await _context.Clients.FirstOrDefaultAsync(x => x.ReasonName == strName && x.CompanyId == intCompanyId);
            return objClient;
        }

        public async Task<Client> UpdateAsync(Client client)
        {
            _context.Clients.Update(client);
            await _context.SaveChangesAsync();

            return client;
        }

        public async Task<bool> AnyClientWithEmailAsync(int companyId, string email, int? clientId)
        {
            if (clientId.HasValue)
                return await _context.Clients
                                .AnyAsync(x => x.CompanyId == companyId &&
                                               x.Email == email &&
                                               x.ClientId != clientId);
            else
                return await _context.Clients
                                .AnyAsync(x => x.CompanyId == companyId &&
                                               x.Email == email);
        }

        public async Task<bool> AnyClientWithNumberRegistrationAsync(int companyId, string registrationNumber, int? clientId)
        {
            if (clientId.HasValue)
                return await _context.Clients
                                .AnyAsync(x => x.CompanyId == companyId &&
                                               x.RegistrationNumber == registrationNumber &&
                                               x.ClientId != clientId);
            else
                return await _context.Clients
                                .AnyAsync(x => x.CompanyId == companyId &&
                                               x.RegistrationNumber == registrationNumber);
        }
    }
}
