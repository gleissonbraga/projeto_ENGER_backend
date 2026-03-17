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
        public long BudgetId { get; private set; }
        public string? Description { get; private set; }
        public Status Status { get; private set; }
        public int? CompanyId { get; private set; }
        public int? ClientId { get; private set; }
        public decimal? TotalStepsValue { get; private set; }
        public decimal? TotalMaterialsValue { get; private set; }
        public decimal? TotalValue { get; private set; }
        public string? Notes { get; private set; }

        public DateTime? EntryDate { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        public Company? Company { get; private set; }
        public Client? Client { get; private set; }

        // 🧠 Constructor
        public Budget(
            int? companyId,
            int? clientId,
            string? description,
            Status status,
            decimal? totalStepsValue,
            decimal? totalMaterialsValue,
            decimal? totalValue,
            string? notes
        )
        {
            CompanyId = companyId;
            ClientId = clientId;
            Description = description;
            Status = status;
            TotalStepsValue = totalStepsValue;
            TotalMaterialsValue = totalMaterialsValue;
            TotalValue = totalValue;
            Notes = notes;
            EntryDate = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

    }
}
