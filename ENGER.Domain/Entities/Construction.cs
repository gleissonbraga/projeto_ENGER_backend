using ENGER.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ENGER.Domain.Entities
{
    public class Construction
    {
        [Key]
        public int ConstructionId { get; private set; }
        public string? Description { get; private set; }
        public int? BudgetId { get; private set; }
        public decimal? TotalPaidValue { get; private set; }
        public decimal? TotalConstructionValue { get; private set; }

        // Address fields
        public string? Street { get; private set; }
        public string? Number { get; private set; }
        public string? City { get; private set; }
        public string? Neighborhood { get; private set; }
        public string? ZipCode { get; private set; }
        public string? StateAbbreviation { get; private set; } // UF
        public string? StateDescription { get; private set; }

        public DateTime? StartDate { get; private set; }
        public DateTime? EstimatedDeliveryDate { get; private set; }
        public DateTime? FinalizationDate { get; private set; }

        public int Status { get; private set; } // SMALLINT no JSON
        public int? CompanyId { get; private set; }
        public int? ResponsibleId { get; private set; }

        // Navigation Properties (Exemplos baseados nos IDs fornecidos)
        public virtual Company? Company { get; private set; }
        public virtual Budget? Budget { get; private set; }

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