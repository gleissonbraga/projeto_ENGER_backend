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
            var requestOptions = new MercadoPago.Client.RequestOptions
            {
                AccessToken = "APP_USR-7991292609269880-030517-e6fdcad023d66c8798f735cfc1dc51a7-3246983468"
            };

            var customerClient = new MercadoPago.Client.Customer.CustomerClient();
            var cardClient = new MercadoPago.Client.Customer.CustomerCardClient();

            var customerRequest = new MercadoPago.Client.Customer.CustomerRequest
            {
                Email = email
            };

            var customer = await customerClient.CreateAsync(customerRequest, requestOptions);
            string customerId = customer.Id;

            CustomerCardCreateRequest cardRequest = new CustomerCardCreateRequest
            {
                Token = cardToken
            };

            CustomerCard card = await cardClient.CreateAsync(customerId, cardRequest, requestOptions);

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
                back_url = ""
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
        public async Task<(int, string)> ReceivedNotification(JsonElement payload)
        {
            int newStatus = 0;
            string externalReference = "";

            if (payload.TryGetProperty("action", out var actionElement) &&
                payload.TryGetProperty("data", out var dataElement))
            {
                string action = actionElement.GetString().ToString();

                if (action == "payment.created" || action == "payment.updated")
                {
                    string paymentId = dataElement.GetProperty("id").GetString();

                    using var httpClient = new HttpClient();
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "APP_USR-7991292609269880-030517-e6fdcad023d66c8798f735cfc1dc51a7-3246983468");

                    var response = await httpClient.GetAsync($"https://api.mercadopago.com/v1/payments/{paymentId}");

                    Console.WriteLine(response);
                    Console.WriteLine(response.IsSuccessStatusCode);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();
                        using var paymentJson = JsonDocument.Parse(responseString);
                        var paymentRoot = paymentJson.RootElement;

                        Console.WriteLine("entrou na validação");

                        string status = paymentRoot.GetProperty("status").GetString();
                        externalReference = paymentRoot.GetProperty("external_reference").GetString();

                        newStatus = status switch
                        {
                            "approved" => (int)Status.SubActive,
                            "rejected" => (int)Status.SubRejected,
                            "pending" => (int)Status.SubPending,
                            "in_process" => (int)Status.SubInProcess,
                            "cancelled" => (int)Status.SubCancelled,
                            _ => (int)Status.SubPending
                        };
                    }
                }
            }


            return (newStatus, externalReference);
        }
    }
}
