
using ENGER.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENGER.Domain.Interfaces.Repositories
{
    public interface ICardRepository
    {
        Task<Card> GetCardByIdCompanyAsync(int companyId);
        Task<Card> AddAsync(Card card);
    }
}


