using ENGER.Application.DTOs.User;
using ENGER.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENGER.Application.UseCases.User.UpdateUser
{
    public class UpdateUserUseCase
    {
        public readonly IUserRepository _repository;

        public UpdateUserUseCase(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserResponseDTO> ExecuteAsync(int userId, int intCompanyId)
        {
            Domain.Entities.User objUser = await _repository.GetByIdAsync(userId, intCompanyId);

            if (objUser == null)
                throw new Exception("Usuário não encontrado");

            UserResponseDTO userDTO = new UserResponseDTO(1, "", "", 1, DateTime.UtcNow, DateTime.UtcNow);

            return userDTO;
        }
    }
}
