using ENGER.Application.DTOs.Login;
using ENGER.Application.Exceptions;
using ENGER.Application.UseCases.Login;
using Microsoft.AspNetCore.Mvc;

namespace ENGER.API.Controllers
{
    [Route("api/")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly LoginUseCase _loginUseCase;

        public LoginController(LoginUseCase loginUseCase)
        {
            _loginUseCase = loginUseCase;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO request)
        {
            try
            {
                var response = await _loginUseCase.ExecuteAsync(request);

                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    //Secure = true, // em produção deve ficar true
                    SameSite = SameSiteMode.None,
                    Expires = DateTime.UtcNow.AddHours(8)
                };

                Response.Cookies.Append("EngerAuthToken", response.Token, cookieOptions);

                return Ok(new
                {
                    userName = response.UserName,
                    adminLevel = response.admin,
                    companyId = response.companyId,
                    subscriptionTypeId = response.typeSubscriptionId,
                    expirationDate = response.expirationDate
                });
            }
            catch (ApplicException err)
            {
                return Unauthorized(new { errors = err.lstErrors });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Erro interno ao processar o login.",
                    detail = ex.Message
                });
            }
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Append("EngerAuthToken", "", new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Path = "/",
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(-1) // Data de ontem
            });

            return Ok(new { message = "Logout realizado com sucesso." });
        }
    }
}
