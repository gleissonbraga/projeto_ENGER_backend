using ENGER.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENGER.Domain.Entities
{
    public class Budget
    {
        [Key]
        public int BudgetId { get; set; }
        public string? Description { get; set; }
        public Status Status { get; set; }
        public int? CompanyId { get; set; }
        public int? ClientId { get; set; }
        public int? UserId { get; set; }
        public decimal? TotalStepsValue { get; set; }
        public decimal? TotalMaterialsValue { get; set; }
        public decimal? TotalValue { get; set; }
        public string? Observation { get; set; }
        public Guid KeyBudget { get; set; }

        public DateTime? EntryDate { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<BudgetStage> Stages { get; set; } = new List<BudgetStage>();
        public Company? Company { get; set; }
        public Client? Client { get; set; }

        public string? Street { get; set; }
        public string? Number { get; set; }
        public string? City { get; set; }
        public string? Neighborhood { get; set; }
        public string? ZipCode { get; set; }
        public string? StateAbbreviation { get; set; }
        public string? StateDescription { get; set; }

        public virtual ICollection<ConstructionStage> StagesConstruction { get; private set; } = new List<ConstructionStage>();
        public virtual ICollection<ConstructionEmployee> Employees { get; private set; } = new List<ConstructionEmployee>();
        public virtual ICollection<ConstructionPayment> Payments { get; private set; } = new List<ConstructionPayment>();
        public virtual ICollection<ConstructionPresence> Presences { get; private set; } = new List<ConstructionPresence>();
        public virtual ICollection<ConstructionRental> Rentals { get; private set; } = new List<ConstructionRental>();
        public virtual ICollection<ConstructionAttachment> Attachments { get; private set; } = new List<ConstructionAttachment>();

        private Budget() { }

        public Budget(
             int? companyId,
             int? clientId,
             int? userId,
             string? description,
             Status status,
             decimal? totalStepsValue,
             decimal? totalMaterialsValue,
             decimal? totalValue,
             string? observation,
             Guid keyBudget,
             string? street,
             string? number,
             string? city,
             string? neighborhood,
             string? zipCode,
             string? stateAbbreviation,
             string? stateDescription
         )
        {
            CompanyId = companyId;
            ClientId = clientId;
            UserId = userId;
            Description = description;
            Status = status;
            TotalStepsValue = totalStepsValue;
            TotalMaterialsValue = totalMaterialsValue;
            TotalValue = totalValue;
            Observation = observation;
            KeyBudget = keyBudget;

            // Endereço da Obra
            Street = street;
            Number = number;
            City = city;
            Neighborhood = neighborhood;
            ZipCode = zipCode;
            StateAbbreviation = stateAbbreviation;
            StateDescription = stateDescription;

            EntryDate = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
