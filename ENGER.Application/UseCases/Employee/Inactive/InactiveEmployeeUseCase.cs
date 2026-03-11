using ENGER.Application.DTOs.Employee;
using ENGER.Application.DTOs.Position;
using ENGER.Application.DTOs.Subsciption;
using ENGER.Application.Exceptions;
using ENGER.Domain.Entities;
using ENGER.Domain.Enums;
using ENGER.Domain.Exceptions;
using ENGER.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ENGER.Application.UseCases.Employee.Inactive
{
    public class InactiveEmployeeUseCase
    {
        private readonly IEmployeeRepository _repository;

        public InactiveEmployeeUseCase(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public async Task<Domain.Entities.Employee> ExecuteAsync(int companyId, int employeeId)
        {
            Domain.Entities.Employee objEmployee = await _repository.GetByIdAsync(employeeId, companyId);

            if (objEmployee == null)
                new ValidationError("employee", "Funcionário não encotrado.");

            objEmployee.Status = Status.Inactive;

            await _repository.InactiveEmployee(objEmployee);

            return objEmployee;
        }
    }
}
