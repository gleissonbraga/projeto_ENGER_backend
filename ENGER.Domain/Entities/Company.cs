using ENGER.Domain.Enums;
using ENGER.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public DateTime EntryDate { get; private set; }
        public DateTime UpdateDate { get; private set; }
        public Guid? SubscriptionCode { get; private set; }

        public virtual ICollection<User> Users { get; private set; } = new List<User>();

        protected Company() { }

        public Company(string reasonName, string fantasyName, string registrationNumber, 
                                string rGIeNumber, string email, string street, string number, string city, string neighborhood, string zipCode,
                                string federativeunit, string phoneNumber, DateTime dateOfBirth, Guid? subscriptionCode)
        {
            Validate(reasonName, fantasyName, registrationNumber, rGIeNumber, email,
                     street, number, city, neighborhood, zipCode, federativeunit, phoneNumber);

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
            SubscriptionCode = subscriptionCode;
            EntryDate = DateTime.UtcNow;
            UpdateDate = DateTime.UtcNow;
        }

        private void Validate(string reasonName, string fantasyName, string registrationNumber,
                               string rGIeNumber, string email, string street, string number,
                               string city, string neighborhood, string zipCode,
                               string federativeunit, string phoneNumber)
        {
            // Exemplo de obrigatoriedade (IsRequired no Mapping)
            if (string.IsNullOrWhiteSpace(registrationNumber))
                throw new DomainException("Inscricao", "O número de registro (CPF/CNPJ) é obrigatório.");

            // Validações de tamanho máximo (HasMaxLength no Mapping)
            if (reasonName?.Length > 100) throw new DomainException("Nome Razão", "Razão Social deve ter no máximo 100 caracteres.");
            if (fantasyName?.Length > 100) throw new DomainException("Nome Fantasia", "Nome Fantasia deve ter no máximo 100 caracteres.");
            if (registrationNumber?.Length > 14) throw new DomainException("Numero", "CPF/CNPJ deve ter no máximo 14 caracteres.");
            if (rGIeNumber?.Length > 14) throw new DomainException("Numero", "RG/IE deve ter no máximo 14 caracteres.");
            if (email?.Length > 50) throw new DomainException("Email", "Email deve ter no máximo 50 caracteres.");
            if (street?.Length > 50) throw new DomainException("Endereco", "Logradouro deve ter no máximo 50 caracteres.");
            if (number?.Length > 50) throw new DomainException("Numero", "Número deve ter no máximo 50 caracteres.");
            if (city?.Length > 40) throw new DomainException("Cidade", "Cidade deve ter no máximo 40 caracteres.");
            if (neighborhood?.Length > 30) throw new DomainException("Bairro", "Bairro deve ter no máximo 30 caracteres.");
            if (zipCode?.Length > 8) throw new DomainException("CEP", "CEP deve ter no máximo 8 caracteres.");
            if (federativeunit?.Length > 2) throw new DomainException("US", "UF deve ter no máximo 2 caracteres.");
            if (phoneNumber?.Length > 14) throw new DomainException("Telefone", "Telefone deve ter no máximo 14 caracteres.");
        }
    }
}
