using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENGER.Domain.Services
{
    public interface IUserContext
    {
        int GetCompanyId();
        int GetUserId();
        string GetUserName();
        int GetAdminLevel();
    }
}
