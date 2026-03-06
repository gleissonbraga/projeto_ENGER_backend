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
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.Json;
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

        public async Task<bool> CancelSubscriptionAsync(string mpSubscriptionId)
        {
            using var httpClient = new HttpClient();

            string accessToken = "TEST-8390326417261248-030317-f61af5e647880f935dc6f37c4d846867-2685085537";
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var requestBody = new
            {
                status = "cancelled"
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PutAsync($"https://api.mercadopago.com/preapproval/{mpSubscriptionId}", content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<(int, string)> CreatePaymentAsync(Card card, decimal amount, Guid subscriptionCode, string cardToken, Company company, Subscription subscription, SubscriptionType subscriptionType)
        {
            using var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer APP_USR-7991292609269880-030517-e6fdcad023d66c8798f735cfc1dc51a7-3246983468");

            var requestBody = new
            {
                reason = subscriptionType.DescriptionSubscriptionType,
                external_reference = subscriptionCode.ToString(),
                payer_email = company.Email,

                card_token_id = cardToken,
                status = "authorized",

                auto_recurring = new
                {
                    frequency = subscriptionType.SubscriptionMonth,
                    frequency_type = "months",
                    transaction_amount = amount,
                    currency_id = "BRL"
                },
                back_url = "https://seusite.com.br/assinatura-concluida"
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("https://api.mercadopago.com/preapproval", content);
            var responseString = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                using var jsonDocument = JsonDocument.Parse(responseString);
                var root = jsonDocument.RootElement;

                string subscriptionId = root.GetProperty("id").GetString();
                string mpStatus = root.GetProperty("status").GetString();

                int status = mpStatus switch
                {
                    "authorized" => (int)Status.SubActive,
                    "pending" => (int)Status.SubPending,
                    "paused" => (int)Status.SubInProcess,
                    "cancelled" => (int)Status.SubCancelled,
                    _ => throw new Exception($"Status de assinatura desconhecido: {mpStatus}")
                };

                return (status, subscriptionId);
            }
            else
            {
                return ((int)Status.SubRejected, "");
            }
        }
    }
}
