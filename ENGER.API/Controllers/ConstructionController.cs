using ENGER.Application.DTOs.Budget;
using ENGER.Application.DTOs.Construction;
using ENGER.Application.Exceptions;
using ENGER.Application.UseCases.Budget.Create;
using ENGER.Application.UseCases.Budget.GetAll;
using ENGER.Application.UseCases.Budget.GetByID;
using ENGER.Application.UseCases.Construction.Create;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ENGER.API.Controllers
{
    [Route("api/obras")]
    [ApiController]
    public class ConstructionController : ControllerBase
    {
        public readonly CreateConstructionUseCase _createConstructionUseCase;

        public ConstructionController(CreateConstructionUseCase createBudgetUseCase)
        {
            _createConstructionUseCase = createBudgetUseCase;
        }


        [HttpPost("{keyBudget}/{companyId}")]
        public async Task<IActionResult> Create([FromRoute] Guid keyBudget, [FromRoute] int companyId)
        {
            try
            {
                Domain.Entities.Construction objBudget = await _createConstructionUseCase.ExecuteAsync(keyBudget, companyId);
                return Ok(objBudget);
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
