using ENGER.Domain.Entities;
using ENGER.Domain.Interfaces.Repositories;
using ENGER.Infrastructure.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENGER.Infrastructure.Repositories
{
    public class CardRepository : ICardRepository
    {
        private readonly AppDbContext _context;

        public CardRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Card> AddAsync(Card card)
        {
            await _context.AddAsync(card);
            await _context.SaveChangesAsync();

            return card;
        }

        public Task<Card> GetCardByIdCompanyAsync(int companyId)
        {
            throw new NotImplementedException();
        }
    }
}
