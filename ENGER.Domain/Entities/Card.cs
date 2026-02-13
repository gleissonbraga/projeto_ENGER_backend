using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENGER.Domain.Entities
{
    public class Card
    {
        [Key]
        public int CardId { get; set; }
        public string MercadoPagoCustomerId { get; private set; }
        public string MercadoPagoCardId { get; private set; }
        public string LastCardNumber { get; set; }
        public string Brand { get; set; }
        public int ExpirationMonth { get; private set; }
        public int ExpirationYear { get; private set; }

        public int CompanyId { get; set; }
        public Company Company { get; set; }

        public Card(
        string mercadoPagoCustomerId,
        string mercadoPagoCardId,
        string lastFourDigits,
        string brand,
        int expirationMonth,
        int expirationYear,
        int companyId)
        {
            MercadoPagoCustomerId = mercadoPagoCustomerId;
            MercadoPagoCardId = mercadoPagoCardId;
            LastCardNumber = lastFourDigits;
            Brand = brand;
            ExpirationMonth = expirationMonth;
            ExpirationYear = expirationYear;
            CompanyId = companyId;
        }
    }
}
