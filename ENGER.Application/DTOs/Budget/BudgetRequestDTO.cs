using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENGER.Application.DTOs.Budget
{
    public record BudgetResponseDTO(
        int? CompanyId,
        int? ClientId,
        string? Description,
        int? Status,
        decimal? TotalStepsValue,
        decimal? TotalMaterialsValue,
        decimal? TotalValue,
        string? Notes,
        List<BudgetStageRequestDTO> Stages
    );
}
