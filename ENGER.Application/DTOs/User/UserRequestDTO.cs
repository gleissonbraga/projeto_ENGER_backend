using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENGER.Application.DTOs.User
{
    public record UserRequestDTO(string username, string email, string password, short? admin);
}
