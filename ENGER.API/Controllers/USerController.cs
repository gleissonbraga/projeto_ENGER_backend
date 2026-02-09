using ENGER.Application.DTOs.Company;
using ENGER.Application.DTOs.User;
using ENGER.Application.UseCases.Company.Create;
using ENGER.Application.UseCases.User.GetAll;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ENGER.API.Controllers
{
    [Route("api/usuarios")]
    [ApiController]
    public class USerController : ControllerBase
    {
        private readonly GetAllUsersUseCase _createUserUseCase;

        public USerController(GetAllUsersUseCase createUserUseCase)
        {
            _createUserUseCase = createUserUseCase;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Create([FromRoute] int id)
        {
            IEnumerable<UserResponseDTO> users = await _createUserUseCase.ExecuteAsync(id);

            // Retorna 201 Created com o ID gerado
            return Ok(users);
        }
    }
}
