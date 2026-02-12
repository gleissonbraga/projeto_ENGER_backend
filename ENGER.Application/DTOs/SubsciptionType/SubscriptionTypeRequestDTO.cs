using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENGER.Application.DTOs.SubsciptionType
{
    public record SubscriptionTypeRequestDTO(string descriptionSubscriptionType, decimal subscriptionValue);
}
