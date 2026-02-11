using ENGER.Application.DTOs.User;
using ENGER.Application.Exceptions;
using ENGER.Domain.Entities;
using ENGER.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENGER.Application.UseCases.User.GetUserById
{
    public class GetUserByIdUseCase
    {
        public readonly IUserRepository _repository;

        public GetUserByIdUseCase(IUserRepository repository) 
        {
            _repository = repository;
        }

        public async Task<UserResponseDTO> ExecuteAsync(int userId, int intCompanyId)
        {
            Domain.Entities.User objUser = await _repository.GetByIdAsync(userId, intCompanyId);

            if (objUser == null)
                throw new ApplicException("user", "Usuário não encontrado");

            UserResponseDTO userDTO = new UserResponseDTO
            (
                objUser.UserId,
                objUser.Username,
                objUser.Email,
                (short)objUser.Admin,
                objUser.EntryDate,
                objUser.UpdateDate,
                (short)objUser.Status
            );

            return userDTO;
        }
    }
}
