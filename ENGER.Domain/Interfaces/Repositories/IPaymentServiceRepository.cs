using ENGER.Domain.Entities;
using ENGER.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ENGER.Domain.Interfaces.Repositories
{
    public interface IPaymentServiceRepository
    {
        Task<Card> AddCardCustomerAsync(string cardToken, string email, int companyId);
        Task<(int, string)> CreatePaymentAsync(Card card, decimal amount, Guid subscriptionCode, string cardToken, Company company, Subscription subscription, SubscriptionType subscriptionType);
        Task<bool> CancelSubscriptionAsync(string mpSubscriptionId);
        Task<(int, string)> ReceivedNotification(JsonElement payload);
    }
}
