using ENGER.Application.DTOs.Company;
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
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ENGER.Application.UseCases.Employee.Update
{
    public class UpdateClientUseCase
    {
        private readonly IClientRepository _repository;

        public UpdateClientUseCase(IClientRepository repository)
        {
            _repository = repository;
        }

        public async Task<Domain.Entities.Client> ExecuteAsync(int intCompanyId, int intClientId, ClientRequestDTO request)
        {
            var errors = new List<ValidationError>();

            Validation.Validation.InputRequired(request.reasonName, "reasonName", errors);
            Validation.Validation.MaxLength(request.reasonName, 100, "reasonName", errors);

            Validation.Validation.InputRequired(request.fantasyName, "fantasyName", errors);
            Validation.Validation.MaxLength(request.fantasyName, 100, "fantasyName", errors);

            Validation.Validation.InputRequired(request.registrationNumber, "registrationNumber", errors);
            Validation.Validation.MaxLength(request.registrationNumber, 14, "registrationNumber", errors);

            Validation.Validation.InputRequired(request.street, "street", errors);
            Validation.Validation.MaxLength(request.street, 30, "street", errors);

            Validation.Validation.InputRequired(request.number, "number", errors);
            Validation.Validation.MaxLength(request.number, 6, "number", errors);

            Validation.Validation.InputRequired(request.city, "city", errors);
            Validation.Validation.MaxLength(request.city, 40, "city", errors);

            Validation.Validation.InputRequired(request.neighborhood, "neighborhood", errors);
            Validation.Validation.MaxLength(request.neighborhood, 16, "neighborhood", errors);

            Validation.Validation.InputRequired(request.zipCode, "zipCode", errors);
            Validation.Validation.MaxLength(request.zipCode, 8, "zipCode", errors);

            Validation.Validation.InputRequired(request.federativeunit, "federativeunit", errors);
            Validation.Validation.MaxLength(request.federativeunit, 2, "federativeunit", errors);

            Validation.Validation.InputRequired(request.phoneNumber, "phoneNumber", errors);
            Validation.Validation.OnlyNumbers(request.phoneNumber, "phoneNumber", errors);
            Validation.Validation.MaxLength(request.phoneNumber, 13, "phoneNumber", errors);

            Validation.Validation.InputRequired(request.cellNumber, "cellNumber", errors);
            Validation.Validation.OnlyNumbers(request.cellNumber, "cellNumber", errors);
            Validation.Validation.MaxLength(request.cellNumber, 13, "cellNumber", errors);

            Validation.Validation.InputRequired(request.email, "email", errors);
            Validation.Validation.MaxLength(request.email, 100, "email", errors);
            Validation.Validation.EmailFormat(request.email, "email", errors);

            Validation.Validation.InputRequired(request.status.ToString(), "status", errors);
            //Validation.Validation.OnlyNumbers((Status)request.status.ToString(), "status", errors);

            Domain.Entities.Client objClient = await _repository.GetByIdAsync(intClientId, intCompanyId);

            bool emailInUse = await _repository.AnyClientWithEmailAsync(intCompanyId, request.email, intClientId);
            bool registrationNumberInUse = await _repository.AnyClientWithNumberRegistrationAsync(intCompanyId, request.registrationNumber, intClientId);

            if (objClient == null) 
                new ValidationError("client", "Cliente não encontrado.");

            if (emailInUse) 
                errors.Add(new ValidationError("email", "Este e-mail já está sendo utilizado por outro cliente."));

            if (registrationNumberInUse) 
                errors.Add(new ValidationError("registrationNumber", "Este CPF já está sendo utilizado por outro cliente."));

            if (errors.Count > 0)
                throw new ApplicException(errors);

            objClient.ReasonName = request.reasonName;
            objClient.FantasyName = request.fantasyName;
            objClient.RegistrationNumber = request.registrationNumber;
            objClient.RGIENumber = request.rGIeNumber;
            objClient.PhoneNumber = request.phoneNumber;
            objClient.CellNumber = request.cellNumber;
            objClient.Email = request.email;
            objClient.FederativeUnit = request.federativeunit;
            objClient.UpdateDate = DateTime.UtcNow;
            objClient.Status = (Status)request.status;
            objClient.City = request.city;
            objClient.Number = request.number;
            objClient.Neighborhood = request.neighborhood;
            objClient.ZipCode = request.zipCode;
            objClient.Street = request.street;

            await _repository.UpdateAsync(objClient);

            return objClient;
        }
    }
}