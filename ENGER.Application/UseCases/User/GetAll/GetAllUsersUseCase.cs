using ENGER.Application.DTOs.Company;
using ENGER.Application.DTOs.User;
using ENGER.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENGER.Application.UseCases.User.GetAll
{
    public class GetAllUsersUseCase
    {
        public readonly IUserRepository _repository;

        public GetAllUsersUseCase(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<UserResponseDTO>> ExecuteAsync(int companyId)
        {
            var users = await _repository.GetByCompanyIdAsync(companyId);

            IEnumerable<UserResponseDTO> usersDTO = users.Select(x => new UserResponseDTO(x.UserId, x.Username, x.Email, (short)x.Admin, x.EntryDate, x.UpdateDate));

            return usersDTO;
        }
    }
}
