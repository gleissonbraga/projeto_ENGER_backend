using ENGER.Application.DTOs.Employee;
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

namespace ENGER.Application.UseCases.Employee.Create
{
    public class CreateEmployeeUseCase
    {
        private readonly IEmployeeRepository _repository;

        public CreateEmployeeUseCase(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public async Task<Domain.Entities.Employee> ExecuteAsync(int companyId, EmployeeRequestDTO request)
        {
            var errors = new List<ValidationError>();

            Validation.Validation.InputRequired(request.employeeName, "employeeName", errors);
            Validation.Validation.MaxLength(request.employeeName, 100, "employeeName", errors);

            Validation.Validation.InputRequired(request.registrationNumber, "registrationNumber", errors);
            Validation.Validation.MaxLength(request.registrationNumber, 11, "registrationNumber", errors);

            Validation.Validation.InputRequired(request.numberGeneralRegistration, "numberGeneralRegistration", errors);
            Validation.Validation.OnlyNumbers(request.numberGeneralRegistration, "numberGeneralRegistration", errors);
            Validation.Validation.MaxLength(request.numberGeneralRegistration, 11, "numberGeneralRegistration", errors);

            Validation.Validation.InputRequired(request.dateOfBirth.ToString(), "dateOfBirth", errors);
            //Validation.Validation.IsDate(request.dateOfBirth.ToString(), "dateOfBirth", errors);

            Validation.Validation.InputRequired(request.admissionDate.ToString(), "admissionDate", errors);
            //Validation.Validation.IsDate(request.admissionDate.ToString(), "admissionDate", errors);

            Validation.Validation.InputRequired(request.phoneNumber, "phoneNumber", errors);
            Validation.Validation.OnlyNumbers(request.phoneNumber, "phoneNumber", errors);
            Validation.Validation.MaxLength(request.phoneNumber, 11, "phoneNumber", errors);

            Validation.Validation.InputRequired(request.cellNumber, "cellNumber", errors);
            Validation.Validation.OnlyNumbers(request.cellNumber, "cellNumber", errors);
            Validation.Validation.MaxLength(request.cellNumber, 11, "cellNumber", errors);

            Validation.Validation.InputRequired(request.positionId.ToString(), "positionId", errors);
            Validation.Validation.OnlyNumbers(request.positionId.ToString(), "positionId", errors);

            Validation.Validation.InputRequired(request.email, "email", errors);
            Validation.Validation.MaxLength(request.email, 100, "email", errors);
            Validation.Validation.EmailFormat(request.email, "email", errors);

            bool emailInUse = await _repository.AnyEmployeeWithEmailAsync(companyId, request.email, null);
            bool registrationNumberInUse = await _repository.AnyEmployeeWithNumberRegistrationAsync(companyId, request.registrationNumber, null);

            if (emailInUse)
                errors.Add(new ValidationError("email", "Este e-mail já está sendo utilizado por outro funcionário."));

            if (registrationNumberInUse)
                errors.Add(new ValidationError("registrationNumber", "Este CPF já está sendo utilizado por outro funcionário."));

            if (errors.Count > 0)
                throw new ApplicException(errors);

            Domain.Entities.Employee objEmployee = new Domain.Entities.Employee(request.employeeName, request.registrationNumber, request.numberGeneralRegistration, 
                                                                                    request.dateOfBirth, request.admissionDate, request.phoneNumber, request.cellNumber, 
                                                                                        request.email, companyId, request.positionId, Domain.Enums.Status.Active);
            await _repository.AddAsync(objEmployee);

            return objEmployee;
        }
    }
}
