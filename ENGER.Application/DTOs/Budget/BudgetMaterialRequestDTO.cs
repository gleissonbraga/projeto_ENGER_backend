using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENGER.Application.DTOs.Budget
{
    public record BudgetMaterialRequestDTO(
        string Description,
        string Unit,
        decimal PlannedQuantity,
        decimal UnitCost,
        bool IsClientProvided
    );
}
