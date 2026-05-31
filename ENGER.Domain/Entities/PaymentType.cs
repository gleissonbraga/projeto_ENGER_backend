using System.ComponentModel.DataAnnotations;

namespace ENGER.Domain.Entities
{
    public class PaymentType
    {
        [Key]
        public int PaymentTypeId { get; private set; }
        public string? Description { get; private set; }

        private PaymentType() { }

        public PaymentType(string? description)
        {
            Description = description;
        }
    }
}