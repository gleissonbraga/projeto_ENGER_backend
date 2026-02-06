using ENGER.Application.DTOs.Company;
using ENGER.Application.DTOs.User;
using ENGER.Domain.Entities;
using ENGER.Domain.Enums;
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

            Domain.Entities.Company company = new Domain.Entities.Company(request.reasonName, request.fantasyName, request.registrationNumber, 
                                                request.rGIeNumber, request.email, request.street, request.number,
                                                request.city, request.neighborhood, request.zipCode, request.federativeunit, 
                                                request.phoneNumber, DateTime.UtcNow, null);

            int companyId = await _repository.AddAsync(company);

            User user = new User(request.username, request.emailUser, request.password, Admin.Master, DateTime.UtcNow, DateTime.UtcNow, companyId);

            User userResponse = await _userRepository.AddAsync(user);

            var userDTO = new UserResponseDTO(userResponse.UserId, userResponse.Username, userResponse.Email, (short)userResponse.Admin, userResponse.EntryDate, userResponse.UpdateDate);

            return new CompanyResponseDTO(company.CompanyId, company.ReasonName, 
                            company.FantasyName, company.RegistrationNumber, company.RGIENumber, 
                            company.Email, company.Street, company.Number, 
                            company.Number, company.Neighborhood, company.ZipCode, company.FederativeUnit, 
                            company.PhoneNumber, company.DateOfBirth, userDTO);
        }
    }
}
