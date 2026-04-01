using System;
using System.ComponentModel.DataAnnotations;

namespace ENGER.Domain.Entities
{
    public class ConstructionPayment
    {
        [Key]
        public int ConstructionPaymentId { get; private set; }
        public DateTime? PaymentDate { get; private set; }
        public int? PaymentTypeId { get; private set; }
        public int? ConstructionId { get; private set; }
        public int? StageId { get; private set; }
        public decimal? PaymentValue { get; private set; }

        // Navigation Properties
        public virtual Construction? Construction { get; private set; }
        public virtual PaymentType? PaymentType { get; private set; }

        private ConstructionPayment() { }

        public ConstructionPayment(
            DateTime? paymentDate,
            int? paymentTypeId,
            int? constructionId,
            int? stageId,
            decimal? paymentValue
        )
        {
            PaymentDate = paymentDate;
            PaymentTypeId = paymentTypeId;
            ConstructionId = constructionId;
            StageId = stageId;
            PaymentValue = paymentValue;
        }
    }
}