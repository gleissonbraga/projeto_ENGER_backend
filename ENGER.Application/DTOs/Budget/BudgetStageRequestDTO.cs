using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENGER.Application.DTOs.Budget
{
    public record BudgetStageRequestDTO(
        string description,
        int order,
        List<BudgetMaterialRequestDTO> Materials,
        List<BudgetLaborRequestDTO> Labors
    );
}
