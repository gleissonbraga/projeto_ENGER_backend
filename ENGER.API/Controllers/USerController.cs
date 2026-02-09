using ENGER.Application.DTOs.Company;
using ENGER.Application.DTOs.User;
using ENGER.Application.Exceptions;
using ENGER.Application.UseCases.Company.Create;
using ENGER.Application.UseCases.User.GetAll;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ENGER.API.Controllers
{
    [Route("api/usuarios")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly CreateUsersUseCase _createUserUseCase;

        public UserController(CreateUsersUseCase createUserUseCase)
        {
            _createUserUseCase = createUserUseCase;
        }

        [HttpPost("cadastro/{intCompanyId}")]
        public async Task<IActionResult> Create([FromBody] UserRequestDTO request, [FromRoute] int intCompanyId)
        {
            try
            {
                UserResponseDTO user = await _createUserUseCase.ExecuteAsync(intCompanyId, request);
                return Ok(user);
            }
            catch (ApplicException err)
            {
                return BadRequest(new { errors = err.lstErrors });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocorreu um erro interno inesperado.", detail = ex.Message });
            }

            
        }
    }
}
