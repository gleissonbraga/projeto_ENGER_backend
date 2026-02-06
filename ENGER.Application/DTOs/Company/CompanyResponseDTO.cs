using ENGER.Application.DTOs.User;
using ENGER.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENGER.Application.DTOs.Company
{
    public record CompanyResponseDTO(int companyId, string reasonName, string fantasyName, string registrationNumber,
                                string rGIeNumber, string email, string street, string number, string city, string neighborhood, string zipCode,
                                string federativeunit, string phoneNumber, DateTime dateOfBirth, UserResponseDTO 
                                user);
}
