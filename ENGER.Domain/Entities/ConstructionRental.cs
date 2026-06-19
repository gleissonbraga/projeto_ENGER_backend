using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ENGER.Domain.Entities
{
    public class ConstructionRental
    {
        [Key]
        public int RentalId { get; set; }
        public string? EquipmentDescription { get; set; }
        public decimal? RentalValue { get; set; }
        public int? DaysCount { get; set; }
        public DateTime? EntryDate { get; set; }
        public DateTime? ExitDate { get; set; }
        public string? ReceivedBy { get; set; }
        public string? ReturnedBy { get; set; }
        public int? ConstructionId { get; set; }

        [JsonIgnore]
        public virtual Construction? Construction { get; set; }

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