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

namespace ENGER.Application.UseCases.Employee.GetById
{
    public class GetByIdEmployeeUseCase
    {
        private readonly IEmployeeRepository _repository;

        public GetByIdEmployeeUseCase(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public async Task<Domain.Entities.Employee> ExecuteAsync(int companyId, int employeeId)
        {
            Domain.Entities.Employee ieEmployee = await _repository.GetByIdAsync(employeeId, companyId);

            return ieEmployee;
        }
    }
}
