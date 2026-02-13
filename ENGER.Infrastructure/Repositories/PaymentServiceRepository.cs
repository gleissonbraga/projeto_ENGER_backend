using ENGER.Domain.Entities;
using ENGER.Domain.Enums;
using ENGER.Domain.Interfaces.Repositories;
using MercadoPago.Client.Customer;
using MercadoPago.Client.Payment;
using MercadoPago.Resource.CardToken;
using MercadoPago.Resource.Customer;
using MercadoPago.Resource.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace ENGER.Infrastructure.Repositories
{
    public class PaymentServiceRepository : IPaymentServiceRepository
    {
        public async Task<Card> AddCardCustomerAsync(string cardToken, string email, int companyId)
        {
            var customerClient = new CustomerClient();

            CustomerRequest customerRequest = new CustomerRequest
            {
                Email = email
            };

            Customer customer = await customerClient.CreateAsync(customerRequest);

            string customerId = customer.Id;

            var cardClient = new CustomerCardClient();

            CustomerCardCreateRequest cardRequest = new CustomerCardCreateRequest
            {
                Token = cardToken
            };

            CustomerCard card =
                await cardClient.CreateAsync(customerId, cardRequest);

            string mercadoPagoCardId = card.Id;
            string lastFour = card.LastFourDigits;
            string brand = card.PaymentMethod.Name;
            int expirationMonth = (int)card.ExpirationMonth;
            int expirationYear = (int)card.ExpirationYear;

            Card dataCard = new Card(customerId, mercadoPagoCardId, lastFour, brand, expirationMonth, expirationYear, companyId);

            return dataCard;
        }
        public async Task<int> CreatePaymentAsync(Card card, decimal amount, Guid subscriptionCode)
        {
            var paymentClient = new PaymentClient();

            var paymentRequest = new PaymentCreateRequest
            {
                TransactionAmount = amount,
                Installments = 1,
                Token = card.MercadoPagoCardId, // <-- AQUI
                PaymentMethodId = card.Brand.ToLower(),
                Payer = new PaymentPayerRequest
                {
                    Type = "customer",
                    Id = card.MercadoPagoCustomerId
                },
                ExternalReference = subscriptionCode.ToString()
            };

            Payment payment = await paymentClient.CreateAsync(paymentRequest);

            int status = payment.Status switch
            {
                "approved" => (int)Status.SubActive,
                "pending" => (int)Status.SubPending,
                "in_process" => (int)Status.SubInProcess,
                "rejected" => (int)Status.SubRejected,
                "cancelled" => (int)Status.SubCancelled,
                "refunded" => (int)Status.SubRefunded,
                "charged_back" => (int)Status.SubCancelled,
                _ => throw new Exception("Status desconhecido")
            };

            return status;
        }
    }
}
