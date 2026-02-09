using ENGER.Domain.Entities;
using ENGER.Domain.Interfaces.Repositories;
using ENGER.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENGER.Infrastructure.Repositories
{
    internal class UserRepository : IUserRepository
    {
        public readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task ActiveUser(User user)
        {
            _context.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task DeleteAsync(int id)
        {
            User objUser = await _context.Users.FindAsync(id);

            if (objUser != null) 
            {
                _context.Users.Remove(objUser);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<User>> GetByCompanyIdAsync(int companyId)
        {
            return await _context.Users.Where(x => x.CompanyId == companyId).ToListAsync();
        }

        public async Task<User> GetByEmail(string email, int companyId)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.CompanyId == companyId && x.Email == email);
        }

        public async Task<User?> GetByIdAsync(int userid, int intCompanyId)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.CompanyId == intCompanyId && x.UserId == userid);
        }

        public async Task InactiveUser(User user)
        {
            _context.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _context.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
