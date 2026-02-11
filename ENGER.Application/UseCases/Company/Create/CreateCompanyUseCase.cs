using ENGER.Application.DTOs.Company;
using ENGER.Application.DTOs.User;
using ENGER.Application.Exceptions;
using ENGER.Domain.Entities;
using ENGER.Domain.Enums;
using ENGER.Domain.Exceptions;
using ENGER.Domain.Interfaces.Repositories;


namespace ENGER.Application.UseCases.Company.Create
{
    public class CreateCompanyUseCase
    {
        private readonly ICompanyRepository _repository;
        private readonly IUserRepository _userRepository;

        public CreateCompanyUseCase(ICompanyRepository repository, IUserRepository userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
        }

        public async Task<CompanyResponseDTO> ExecuteAsync(CompanyRequestDTO request)
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
            Validation.Validation.MaxLength(request.city, 6, "city", errors);

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

            Domain.Entities.Company objVerifyReasonName = await _repository.GetByReasonNameAsync(request.reasonName);
            Domain.Entities.Company objVerifyIERG = await _repository.GetByNumberIERGAsync(request.rGIeNumber);
            Domain.Entities.Company objVerifyCPFCNPJ = await _repository.GetByCPFCNPJAsync(request.registrationNumber);
            Domain.Entities.Company objVerifyEmail = await _repository.GetByAddressEmailAsync(request.email);

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

            Domain.Entities.Company company = new Domain.Entities.Company(request.reasonName, request.fantasyName, request.registrationNumber, 
                                                request.rGIeNumber, request.email, request.street, request.number,
                                                request.city, request.neighborhood, request.zipCode, request.federativeunit, 
                                                request.phoneNumber, DateTime.UtcNow, null);

            int companyId = await _repository.AddAsync(company);

            Domain.Entities.User user = new Domain.Entities.User(request.username, request.emailUser, request.password, Admin.Master, DateTime.UtcNow, DateTime.UtcNow, companyId, Status.Active);

            Domain.Entities.User userResponse = await _userRepository.AddAsync(user);

            var userDTO = new UserResponseDTO(userResponse.UserId, userResponse.Username, userResponse.Email, (short)userResponse.Admin, userResponse.EntryDate, userResponse.UpdateDate, (short)userResponse.Status);

            return new CompanyResponseDTO(company.CompanyId, company.ReasonName, 
                            company.FantasyName, company.RegistrationNumber, company.RGIENumber, 
                            company.Email, company.Street, company.Number, 
                            company.Number, company.Neighborhood, company.ZipCode, company.FederativeUnit, 
                            company.PhoneNumber, company.DateOfBirth, userDTO);
        }
    }
}
