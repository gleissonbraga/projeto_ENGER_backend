using ENGER.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENGER.Domain.Services;

public interface ITokenService
{
    string GenerateToken(User user, int subscriptionTypeId);
}
