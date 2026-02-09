using ENGER.Application.DTOs.Company;
using ENGER.Application.DTOs.User;
using ENGER.Application.Exceptions;
using ENGER.Domain.Enums;
using ENGER.Domain.Exceptions;
using ENGER.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ENGER.Application.UseCases.User.GetAll
{
    public class CreateUsersUseCase
    {
        public readonly IUserRepository _repository;

        public CreateUsersUseCase(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserResponseDTO> ExecuteAsync(int companyId, UserRequestDTO request)
        {
            var errors = new List<ValidationError>();

            Validation.Validation.InputRequired(request.username, "username", errors);
            Validation.Validation.MaxLength(request.username, 50, "username", errors);

            Validation.Validation.EmailFormat(request.email, "email", errors);

            Validation.Validation.InputRequired(request.password, "password", errors);
            Validation.Validation.MaxLength(request.password, 60, "password", errors);

            Validation.Validation.MaxAdmin(request.admin, "admin", errors);

            Domain.Entities.User objUserEmailVerify = await _repository.GetByEmail(request.email, companyId);

            if(objUserEmailVerify != null)
                errors.Add(new ValidationError("email", "Email já cadastrado"));

            if (errors.Count > 0)
                throw new ApplicException(errors);

            Domain.Entities.User objUser = new Domain.Entities.User(request.username, request.email, 
                                                            request.password, (ENGER.Domain.Enums.Admin)request.admin, 
                                                            DateTime.UtcNow, DateTime.UtcNow, companyId);

            await _repository.AddAsync(objUser);

            UserResponseDTO userDTO = new UserResponseDTO
             (
                 objUser.UserId,
                 objUser.Username,
                 objUser.Email,
                 (short)objUser.Admin,
                 objUser.EntryDate,
                 objUser.UpdateDate
             );

            return userDTO;
        }
    }
}
