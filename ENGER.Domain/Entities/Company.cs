using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ENGER.Domain.Enums;

namespace ENGER.Domain.Entities
{
    public class Company
    {
        [Key]
        public int CompanyId { get; private set; }
        public string ReasonName { get; private set; }
        public string FantasyName { get; private set; }
        public string RegistrationNumber { get; private set; }
        public string RGIENumber { get; private set; }
        public string Email { get; private set; }
        public string Street { get; private set; }
        public string Number { get; private set; }
        public string City { get; private set; }
        public string Neighborhood { get; private set; }
        public string ZipCode { get; private set; }
        public string FederativeUnit { get; private set; }
        public string PhoneNumber { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public Admin Admin { get; private set; }
        public DateTime EntryDate { get; private set; }
        public DateTime UpdateDate { get; private set; }
        public Guid SubscriptionCode { get; private set; }

        protected Company() { }

        public Company(string reasonName, string fantasyName, string registrationNumber, 
                                string rGIeNumber, string email, string street, string number, string city, string neighborhood, string zipCode,
                                string federativeunit, string phoneNumber, DateTime dateOfBirth, Admin admin, Guid subscriptionCode)
        {
            ReasonName = reasonName;
            FantasyName = fantasyName;
            RegistrationNumber = registrationNumber;
            RGIENumber = rGIeNumber;
            Email = email;
            Street = street;
            Number = number;
            City = city;
            Neighborhood = neighborhood;
            ZipCode = zipCode;
            FederativeUnit = federativeunit;
            PhoneNumber = phoneNumber;
            DateOfBirth = dateOfBirth;
            Admin = admin;
            SubscriptionCode = subscriptionCode;
            EntryDate = DateTime.UtcNow;
            UpdateDate = DateTime.UtcNow;
        }
    }
}
