using ENGER.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ENGER.Domain.Entities
{
    public class Construction
    {
        [Key]
        public int ConstructionId { get;  set; }
        public string? Description { get;  set; }
        public int? BudgetId { get;  set; }
        public decimal? TotalPaidValue { get;  set; }
        public decimal? TotalConstructionValue { get;  set; }

        // Address fields
        public string? Street { get;  set; }
        public string? Number { get;  set; }
        public string? City { get;  set; }
        public string? Neighborhood { get;  set; }
        public string? ZipCode { get;  set; }
        public string? StateAbbreviation { get;  set; } // UF
        public string? StateDescription { get;  set; }

        public DateTime? StartDate { get;  set; }
        public DateTime? EstimatedDeliveryDate { get;  set; }
        public DateTime? FinalizationDate { get;  set; }

        public int Status { get;  set; } // SMALLINT no JSON
        public int? CompanyId { get;  set; }
        public int? ResponsibleId { get;  set; }

        public virtual ICollection<ConstructionStage> Stages { get;  set; } = new List<ConstructionStage>();
        public virtual ICollection<ConstructionEmployee> Employees { get;  set; } = new List<ConstructionEmployee>();
        public virtual ICollection<ConstructionPayment> Payments { get;  set; } = new List<ConstructionPayment>();
        public virtual ICollection<ConstructionPresence> Presences { get;  set; } = new List<ConstructionPresence>();
        public virtual ICollection<ConstructionRental> Rentals { get;  set; } = new List<ConstructionRental>();
        public virtual ICollection<ConstructionAttachment> Attachments { get;  set; } = new List<ConstructionAttachment>();

        public virtual Company? Company { get;  set; }
        public virtual Budget? Budget { get;  set; }

        private Construction() { }

        // 🧠 Constructor
        public Construction(
            string? description,
            int? budgetId,
            decimal? totalPaidValue,
            decimal? totalConstructionValue,
            string? street,
            string? number,
            string? city,
            string? neighborhood,
            string? zipCode,
            string? stateAbbreviation,
            string? stateDescription,
            DateTime? startDate,
            DateTime? estimatedDeliveryDate,
            DateTime? finalizationDate,
            int status,
            int? companyId,
            int? responsibleId
        )
        {
            Description = description;
            BudgetId = budgetId;
            TotalPaidValue = totalPaidValue;
            TotalConstructionValue = totalConstructionValue;
            Street = street;
            Number = number;
            City = city;
            Neighborhood = neighborhood;
            ZipCode = zipCode;
            StateAbbreviation = stateAbbreviation;
            StateDescription = stateDescription;
            StartDate = startDate;
            EstimatedDeliveryDate = estimatedDeliveryDate;
            FinalizationDate = finalizationDate;
            Status = status;
            CompanyId = companyId;
            ResponsibleId = responsibleId;
        }
    }
}