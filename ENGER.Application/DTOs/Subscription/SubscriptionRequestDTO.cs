using ENGER.Application.DTOs.Card;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENGER.Application.DTOs.Subsciption
{
    public record SubscriptionRequestDTO(int subscriptionTypeId, int companyId, CardRequestDTO CardRequestDTO);
  
}
