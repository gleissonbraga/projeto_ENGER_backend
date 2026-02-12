using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENGER.Application.DTOs.Card
{
    public record CardRequestDTO(string cardToken, string lastCardNumber, string brand, string expirationDateCard);
}
