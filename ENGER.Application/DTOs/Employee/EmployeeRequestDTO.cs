using ENGER.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENGER.Application.DTOs.Employee
{
    public record EmployeeRequestDTO(string employeeName, string registrationNumber,
        string numberGeneralRegistration, DateTime dateOfBirth, DateTime admissionDate,
        string phoneNumber, string cellNumber, string email, DateTime entryDate, DateTime updateDate, int positionId, Status? status);
}
