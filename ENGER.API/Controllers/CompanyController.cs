using ENGER.Application.DTOs.Company;
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
            var id = await _createCompanyUseCase.ExecuteAsync(command);

            // Retorna 201 Created com o ID gerado
            return CreatedAtAction(nameof(Create), new { id = id }, id);
        }
    }
}
