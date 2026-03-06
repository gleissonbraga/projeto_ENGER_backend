using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENGER.Domain.Entities
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string RegistrationNumber { get; set; }
        public string NumberGeneralRegistration { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime AdmissionDate { get; set; }
        public string PhoneNumber { get; set; }
        public string CellNumber { get; set; }
        public string Email { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }

        protected Employee() { }

        public Employee(
        string employeeName,
        string registrationNumber,
        string numberGeneralRegistration,
        DateTime dateOfBirth,
        DateTime admissionDate,
        string phoneNumber,
        string cellNumber,
        string email,
        int companyId
    )
        {
            EmployeeName = employeeName;
            RegistrationNumber = registrationNumber;
            NumberGeneralRegistration = numberGeneralRegistration;
            DateOfBirth = dateOfBirth;
            AdmissionDate = admissionDate;
            PhoneNumber = phoneNumber;
            CellNumber = cellNumber;
            Email = email;
            CompanyId = companyId;

            EntryDate = DateTime.UtcNow;
            UpdateDate = DateTime.UtcNow;
        }
    }
}