using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENGER.Application.DTOs.Login
{
    public record LoginRequestDTO(string Email, string Password);
}
