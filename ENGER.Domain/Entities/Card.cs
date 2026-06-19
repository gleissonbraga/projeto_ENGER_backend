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
        public int CardId { get; private set; }

        public string MercadoPagoCustomerId { get; private set; }
        public string MercadoPagoCardId { get; private set; }
        public string LastCardNumber { get; private set; }
        public string Brand { get; private set; }
        public int ExpirationMonth { get; private set; }
        public int ExpirationYear { get; private set; }

        public int CompanyId { get; private set; }
        public Company Company { get; private set; }

        private Card() { }

        public Card(
            string mercadoPagoCustomerId,
            string mercadoPagoCardId,
            string lastCardNumber,
            string brand,
            int expirationMonth,
            int expirationYear,
            int companyId)
        {
            MercadoPagoCustomerId = mercadoPagoCustomerId;
            MercadoPagoCardId = mercadoPagoCardId;
            LastCardNumber = lastCardNumber;
            Brand = brand;
            ExpirationMonth = expirationMonth;
            ExpirationYear = expirationYear;
            CompanyId = companyId;
        }
    }
}
