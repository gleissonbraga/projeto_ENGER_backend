using ENGER.Application.DTOs.Company;
using ENGER.Domain.Entities;
using ENGER.Domain.Enums;
using ENGER.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENGER.Application.UseCases.Company.Create
{
    public class CreateCompanyUseCase
    {
        private readonly ICompanyRepository _repository;

        public CreateCompanyUseCase(ICompanyRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> ExecuteAsync(CreateCompanyRequest request)
        {
            if (!Enum.IsDefined(typeof(Admin), request.admin))
            {
                throw new Exception("Tipo de administrador inválido.");
            }

            Admin adminEnum = (Admin)request.admin;

            var company = new Domain.Entities.Company(request.reasonName, request.fantasyName, request.registrationNumber, 
                                                request.rGIeNumber, request.email, request.street, request.number,
                                                request.city, request.neighborhood, request.zipCode, request.federativeunit, 
                                                request.phoneNumber, DateTime.UtcNow, adminEnum, Guid.NewGuid());

            await _repository.AddAsync(company);
            return company.CompanyId;
        }
    }
}
