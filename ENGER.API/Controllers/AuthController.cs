using ENGER.Application.UseCases.Auth.GetLoggedUser;
using ENGER.Application.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENGER.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly GetLoggedUserUseCase _getLoggedUserUseCase;

    public AuthController(GetLoggedUserUseCase getLoggedUserUseCase)
    {
        _getLoggedUserUseCase = getLoggedUserUseCase;
    }

    [HttpGet("me")]
    [Authorize]
    public IActionResult GetMe()
    {
        try
        {
            var userProfile = _getLoggedUserUseCase.Execute();

            return Ok(userProfile);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                message = "Erro interno ao recuperar dados do usuário autenticado.",
                detail = ex.Message
            });
        }
    }
}