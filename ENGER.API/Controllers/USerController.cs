using ENGER.Application.DTOs.Company;
using ENGER.Application.DTOs.User;
using ENGER.Application.Exceptions;
using ENGER.Application.UseCases.Company.Create;
using ENGER.Application.UseCases.User.Create;
using ENGER.Application.UseCases.User.GetAll;
using ENGER.Application.UseCases.User.GetUserById;
using ENGER.Application.UseCases.User.UpdateUser;
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
        private readonly GetAllUsersUseCase _getAllUserUseCase;
        private readonly GetUserByIdUseCase _getUserByIdUseCase;
        private readonly UpdateUserUseCase _updateUserUseCase;

        public UserController(CreateUsersUseCase createUserUseCase, GetAllUsersUseCase getAllUserUseCase, GetUserByIdUseCase getUserByIdUseCase, UpdateUserUseCase updateUserUseCase)
        {
            _createUserUseCase = createUserUseCase;
            _getAllUserUseCase = getAllUserUseCase;
            _getUserByIdUseCase = getUserByIdUseCase;
            _updateUserUseCase = updateUserUseCase;
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

        [HttpGet("{intCompanyId}")]
        public async Task<IActionResult> GetAll([FromRoute] int intCompanyId)
        {
            try
            {
                IEnumerable<UserResponseDTO> user = await _getAllUserUseCase.ExecuteAsync(intCompanyId);
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

        [HttpGet("{userId}/{intCompanyId}")]
        public async Task<IActionResult> GetUserbyId([FromRoute] int intCompanyId, [FromRoute] int userId)
        {
            try
            {
                UserResponseDTO user = await _getUserByIdUseCase.ExecuteAsync(userId, intCompanyId);
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

        [HttpPut("atualizar/{userId}/{intCompanyId}")]
        public async Task<IActionResult> Update([FromBody] UserRequestDTO request, [FromRoute] int intCompanyId, [FromRoute] int userId)
        {
            try
            {
                UserResponseDTO user = await _updateUserUseCase.ExecuteAsync(request, userId, intCompanyId);
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
