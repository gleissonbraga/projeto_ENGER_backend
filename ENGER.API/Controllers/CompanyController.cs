using ENGER.Application.DTOs.Company;
using ENGER.Application.Exceptions;
using ENGER.Application.UseCases.Company.Create;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ENGER.API.Controllers
{
    [Route("api/empresas")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly CreateCompanyUseCase _createCompanyUseCase;

        public CompanyController(CreateCompanyUseCase createCompanyUseCase)
        {
            _createCompanyUseCase = createCompanyUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CompanyRequestDTO command)
        {
            try
            {
                CompanyResponseDTO objCompany = await _createCompanyUseCase.ExecuteAsync(command);
                return Ok(objCompany);
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
