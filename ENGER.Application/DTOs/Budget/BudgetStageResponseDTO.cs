using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENGER.Application.DTOs.Budget
{
    public record BudgetStageResponseDTO(
           int StageId,
           string Description,
           int Order,
           IEnumerable<BudgetMaterialResponseDTO> Materials,
           IEnumerable<BudgetLaborResponseDTO> Labors
       );
}
