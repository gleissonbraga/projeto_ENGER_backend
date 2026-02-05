using ENGER.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENGER.Application.DTOs.Company
{
    public record CreateCompanyRequest(string reasonName, string fantasyName, string registrationNumber,
                                string rGIeNumber, string email, string street, string number, string city, string neighborhood, string zipCode,
                                string federativeunit, string phoneNumber, DateTime dateOfBirth, int admin);
}
