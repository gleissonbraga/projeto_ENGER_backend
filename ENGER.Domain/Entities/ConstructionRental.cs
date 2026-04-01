using System;
using System.ComponentModel.DataAnnotations;

namespace ENGER.Domain.Entities
{
    public class ConstructionRental
    {
        [Key]
        public int RentalId { get; private set; }
        public string? EquipmentDescription { get; private set; }
        public decimal? RentalValue { get; private set; }
        public int? DaysCount { get; private set; }
        public DateTime? EntryDate { get; private set; }
        public DateTime? ExitDate { get; private set; }
        public string? ReceivedBy { get; private set; }
        public string? ReturnedBy { get; private set; }
        public int? ConstructionId { get; private set; }

        public virtual Construction? Construction { get; private set; }

        private ConstructionRental() { }

        public ConstructionRental(
            string? equipmentDescription,
            decimal? rentalValue,
            int? daysCount,
            DateTime? entryDate,
            DateTime? exitDate,
            string? receivedBy,
            string? returnedBy,
            int? constructionId
        )
        {
            EquipmentDescription = equipmentDescription;
            RentalValue = rentalValue;
            DaysCount = daysCount;
            EntryDate = entryDate;
            ExitDate = exitDate;
            ReceivedBy = receivedBy;
            ReturnedBy = returnedBy;
            ConstructionId = constructionId;
        }
    }
}