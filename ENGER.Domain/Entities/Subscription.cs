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
        public DateTime ExpirationDate { get; set; }
        public Status StatusSubscription { get; set; }
        public DateTime? PaymentDate { get; set; }

        public int TypeSubscriptionId { get; set; }

        public virtual SubscriptionType SubscriptionType { get; private set; }

        protected Subscription() { }

        public Subscription(Guid subscriptionCode, DateTime expirationDate, Status statusSubscription, DateTime? paymentDate, int subscriptionTypeId)
        {
            SubscriptionCode = subscriptionCode;
            ExpirationDate = expirationDate;
            StatusSubscription = statusSubscription;
            PaymentDate = paymentDate;
            TypeSubscriptionId = subscriptionTypeId;
        }
    }
}
