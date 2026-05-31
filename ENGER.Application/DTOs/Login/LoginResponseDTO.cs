using ENGER.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENGER.Application.DTOs.Login
{
    public record LoginResponseDTO(string Token, string UserName, Admin admin, int companyId, DateTime expirationDate, int typeSubscriptionId);
}
