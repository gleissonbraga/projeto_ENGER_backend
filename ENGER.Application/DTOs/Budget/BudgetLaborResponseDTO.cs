using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENGER.Application.DTOs.Budget
{
    public record BudgetLaborResponseDTO(
        int BudgetLaborId,
        int RoleId,
        decimal PlannedHours,
        decimal HourlyRate,
        decimal SocialCharges
    );
}
