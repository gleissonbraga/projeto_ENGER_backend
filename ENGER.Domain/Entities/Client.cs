using ENGER.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENGER.Domain.Entities
{
    public class Client
    {
        [Key]
        public int ClientId { get; set; }
        public string ReasonName { get; set; }
        public string FantasyName { get; set; }
        public string RegistrationNumber { get; set; }
        public string RGIENumber { get; set; }
        public string Email { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string City { get; set; }
        public string Neighborhood { get; set; }
        public string ZipCode { get; set; }
        public string FederativeUnit { get; set; }
        public string PhoneNumber { get; set; }
        public string CellNumber { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public int CompanyId { get; set; }
        public Status Status { get; set; }
        public Company Company { get; set; }

        protected Client() { }

        public Client(
        string reasonName,
        string fantasyName,
        string registrationNumber,
        string rgieNumber,
        string email,
        string street,
        string number,
        string city,
        string neighborhood,
        string zipCode,
        string federativeUnit,
        string phoneNumber,
        string cellNumber,
        Status status,
        int companyId
    )
        {
            ReasonName = reasonName;
            FantasyName = fantasyName;
            RegistrationNumber = registrationNumber;
            RGIENumber = rgieNumber;
            Email = email;
            Street = street;
            Number = number;
            City = city;
            Neighborhood = neighborhood;
            ZipCode = zipCode;
            FederativeUnit = federativeUnit;
            PhoneNumber = phoneNumber;
            CellNumber = cellNumber;
            CompanyId = companyId;
            Status = status;

            EntryDate = DateTime.UtcNow;
            UpdateDate = DateTime.UtcNow;
        }
    }
}
