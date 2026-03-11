using ENGER.Application.DTOs.Position;
using ENGER.Application.DTOs.Subsciption;
using ENGER.Application.Exceptions;
using ENGER.Domain.Entities;
using ENGER.Domain.Exceptions;
using ENGER.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ENGER.Application.UseCases.Employee.GetAll
{
    public class GetAllEmployeesByCompanyUseCase
    {
        private readonly IEmployeeRepository _repository;

        public GetAllEmployeesByCompanyUseCase(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Domain.Entities.Employee>> ExecuteAsync(int companyId)
        {
            IEnumerable<Domain.Entities.Employee> ieEmployees = await _repository.GetByCompanyIdAsync(companyId);

            return ieEmployees;
        }
    }
}
