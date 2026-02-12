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
        public string CardToken { get; set; }
        public string LastCardNumber { get; set; }
        public string Brand { get; set; }
        public string ExpirationDateCard { get; set; }

        public int CompanyId { get; set; }
        public Company Company { get; set; }

        public Card(string cardToken, string lastCardNumber, string brand, string expirationDateCard, int companyId)
        {
            CardToken = cardToken;
            LastCardNumber = lastCardNumber;
            Brand = brand;
            ExpirationDateCard = expirationDateCard;
            CompanyId = companyId;
        }
    }
}
