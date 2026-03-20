using ENGER.Application.DTOs.User;
using ENGER.Application.Exceptions;
using ENGER.Domain.Entities;
using ENGER.Domain.Enums;
using ENGER.Domain.Exceptions;
using ENGER.Domain.Interfaces.Repositories;
using BCrypt.Net;

namespace ENGER.Application.UseCases.User.UpdateUser
{
    public class UpdateUserUseCase
    {
        public readonly IUserRepository _repository;

        public UpdateUserUseCase(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserResponseDTO> ExecuteAsync(UserRequestDTO request, int userId, int intCompanyId)
        {
            Domain.Entities.User objUser = await _repository.GetByIdAsync(userId, intCompanyId);

            if (objUser == null)
                throw new ApplicException("user", "Usuário não encontrado");

            var errors = new List<ValidationError>();

            Validation.Validation.InputRequired(request.username, "username", errors);
            Validation.Validation.MaxLength(request.username, 50, "username", errors);

            Validation.Validation.EmailFormat(request.email, "email", errors);

            Validation.Validation.InputRequired(request.password, "password", errors);
            Validation.Validation.MaxLength(request.password, 60, "password", errors);

            Validation.Validation.MaxEnum(request.admin, "admin", 7, errors);

            Validation.Validation.MaxEnum(request.status, "status", 2, errors);

            Domain.Entities.User objUserEmailVerify = await _repository.GetByEmail(request.email, intCompanyId);

            if(objUserEmailVerify != null && objUser.UserId != objUserEmailVerify.UserId)
                    errors.Add(new ValidationError("email", "Email já cadastrado"));

            if (errors.Count > 0)
                throw new ApplicException(errors);

            string strHashPassword = BCrypt.Net.BCrypt.HashPassword(request.password);

            objUser.Username = request.username;
            objUser.Email = request.email;
            objUser.Password = strHashPassword;
            objUser.UpdateDate = DateTime.UtcNow;
            objUser.Admin = (Admin)request.admin;
            objUser.Status = (Status)request.status;

            Domain.Entities.User objuserResponse = await _repository.UpdateAsync(objUser);

            UserResponseDTO userDTO = new UserResponseDTO(objuserResponse.UserId, objuserResponse.Username, objuserResponse.Email, 
                (short)objuserResponse.Admin, objuserResponse.EntryDate, objuserResponse.UpdateDate, (short)objuserResponse.Status);

            return userDTO;
        }
    }
}
