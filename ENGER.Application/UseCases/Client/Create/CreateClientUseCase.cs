using ENGER.Application.DTOs.Company;
using ENGER.Application.DTOs.User;
using ENGER.Application.Exceptions;
using ENGER.Domain.Entities;
using ENGER.Domain.Enums;
using ENGER.Domain.Exceptions;
using ENGER.Domain.Interfaces.Repositories;


namespace ENGER.Application.UseCases.Client.Create
{
    public class CreateClientUseCase
    {
        private readonly IClientRepository _repository;

        public CreateClientUseCase(IClientRepository repository)
        {
            _repository = repository;
        }

        public async Task<ClientResponseDTO> ExecuteAsync(ClientRequestDTO request, int intCompanyId)
        {

            var errors = new List<ValidationError>();

            Validation.Validation.InputRequired(request.reasonName, "reasonName", errors);
            Validation.Validation.MaxLength(request.reasonName, 100, "reasonName", errors);

            Validation.Validation.InputRequired(request.fantasyName, "fantasyName", errors);
            Validation.Validation.MaxLength(request.fantasyName, 100, "fantasyName", errors);

            Validation.Validation.InputRequired(request.registrationNumber, "registrationNumber", errors);
            Validation.Validation.MaxLength(request.registrationNumber, 14, "registrationNumber", errors);

            Validation.Validation.EmailFormat(request.email, "email", errors);

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
            Validation.Validation.MaxLength(request.phoneNumber, 13, "phoneNumber", errors);

            if (errors.Count > 0)
                throw new ApplicException(errors);

            Domain.Entities.Client objVerifyReasonName = await _repository.GetByReasonNameAsync(request.reasonName, intCompanyId);
            Domain.Entities.Client objVerifyIERG = await _repository.GetByNumberIERGAsync(request.rGIeNumber, intCompanyId);
            Domain.Entities.Client objVerifyCPFCNPJ = await _repository.GetByCPFCNPJAsync(request.registrationNumber, intCompanyId);
            Domain.Entities.Client objVerifyEmail = await _repository.GetByAddressEmailAsync(request.email, intCompanyId);

            if (objVerifyReasonName != null)
                errors.Add(new ValidationError("reasonName", "Nome Razão já cadastrada"));
            if (objVerifyIERG != null)
                errors.Add(new ValidationError("resistrationNumber", "Número de Inscrição já cadastrada"));
            if (objVerifyCPFCNPJ != null)
                errors.Add(new ValidationError("rGIeNumber", "Número de Inscrição já cadastrada"));
            if (objVerifyEmail != null)
                errors.Add(new ValidationError("email", "Email já cadastrado"));

            if (errors.Count > 0)
                throw new ApplicException(errors);

            Domain.Entities.Client objClient = new Domain.Entities.Client(request.reasonName, request.fantasyName, request.registrationNumber, 
                                                request.rGIeNumber, request.email, request.street, request.number,
                                                request.city, request.neighborhood, request.zipCode, request.federativeunit, 
                                                request.phoneNumber, request.cellNumber, Status.Active, intCompanyId);

            await _repository.AddAsync(objClient);

            return new ClientResponseDTO(objClient.CompanyId, objClient.ReasonName,
                            objClient.FantasyName, objClient.RegistrationNumber, objClient.RGIENumber,
                            objClient.Email, objClient.Street, objClient.Number,
                            objClient.Number, objClient.Neighborhood, objClient.ZipCode, objClient.FederativeUnit,
                            objClient.PhoneNumber, objClient.CellNumber);
        }
    }
}
