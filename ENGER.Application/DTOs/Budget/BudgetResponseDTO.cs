using ENGER.Application.DTOs.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENGER.Application.DTOs.Budget
{
    public record BudgetResponseDTO(
     long BudgetId,
     string? Description,
     string Status,
     int? CompanyId,
     ClientResponseDTO? Client,
     int? UserId,
     decimal? TotalStepsValue,
     decimal? TotalMaterialsValue,
     decimal? TotalValue,
     string? Observation,
     DateTime? EntryDate,
     DateTime? UpdatedAt,
     IEnumerable<BudgetStageResponseDTO> Stages
 );
}
