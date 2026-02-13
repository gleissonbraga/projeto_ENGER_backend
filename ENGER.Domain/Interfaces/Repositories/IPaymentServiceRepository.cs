using ENGER.Domain.Entities;
using ENGER.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENGER.Domain.Interfaces.Repositories
{
    public interface IPaymentServiceRepository
    {
        Task<Card> AddCardCustomerAsync(string cardToken, string email, int companyId);
        Task<int> CreatePaymentAsync(Card card, decimal amount, Guid subscriptionCode);
    }
}
