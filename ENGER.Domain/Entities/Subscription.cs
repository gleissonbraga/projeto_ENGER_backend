using ENGER.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENGER.Domain.Entities
{
    public class Subscription
    {

        [Key]
        public int SubscriptionId { get; set; }
        public Guid SubscriptionCode { get; set; }
        public Status StatusSubscription { get; set; }
        public DateTime? PaymentDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime NextBillingDate { get; set; }

        public int TypeSubscriptionId { get; set; }

        public virtual SubscriptionType SubscriptionType { get; private set; }

        protected Subscription() { }

        public Subscription(Guid subscriptionCode, DateTime startDate, Status statusSubscription, DateTime? paymentDate, int subscriptionTypeId, DateTime nextBillingDate, DateTime expirationDate)
        {
            SubscriptionCode = subscriptionCode;
            StatusSubscription = statusSubscription;
            PaymentDate = paymentDate;
            TypeSubscriptionId = subscriptionTypeId;
            NextBillingDate = nextBillingDate;
            ExpirationDate = expirationDate;
        }
    }
}
