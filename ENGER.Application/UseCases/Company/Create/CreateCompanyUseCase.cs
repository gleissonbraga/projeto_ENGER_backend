using ENGER.Domain.Entities;
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

        public async Task<int> ExecuteAsync(CreateCompanyCommand command)
        {
            var company = new Domain.Entities.Company(command.CodigoAssinatura);
            await _repository.AddAsync(company);
            return company.CodigoEmpresa;
        }
    }
}
