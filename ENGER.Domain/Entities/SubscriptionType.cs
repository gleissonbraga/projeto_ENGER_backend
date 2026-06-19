using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENGER.Domain.Entities
{
    public class SubscriptionType
    {
        [Key]
        public int SubscriptionTypeId { get; set; }
        public string DescriptionSubscriptionType { get; set; }
        public decimal SubscriptionValue { get; set; }
        public int SubscriptionMonth { get; set; }

        protected SubscriptionType() { }

        public SubscriptionType(string descriptionSubscriptionType, decimal subscriptionValue, int subscriptionMonth)
        {
            DescriptionSubscriptionType = descriptionSubscriptionType;
            SubscriptionValue = subscriptionValue;
            SubscriptionMonth = subscriptionMonth;
        }
    }
}
