using ENGER.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENGER.Application.DTOs.User
{
    public record UserResponseDTO(int userId, string username, string email, short admin, DateTime entryDate, DateTime updateDate, short status);
}
