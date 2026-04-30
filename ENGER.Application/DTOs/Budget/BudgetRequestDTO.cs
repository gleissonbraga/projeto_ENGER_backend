using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENGER.Application.DTOs.Budget
{
    public record BudgetRequestDTO(
        int? clientId,
        int? userId,
        string? description,
        int? status,
        decimal? totalStepsValue,
        decimal? totalMaterialsValue,
        decimal? totalValue,
        string? observation,
        string? street,
        string? number,
        string? city,
        string? neighborhood,
        string? zipCode,
        string? stateAbbreviation, // UF
        string? stateDescription,
        List<BudgetStageRequestDTO> Stages
    );
}
