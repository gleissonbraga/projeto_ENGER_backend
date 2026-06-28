using ENGER.Application.DTOs.Construction;
using ENGER.Application.Exceptions;
using ENGER.Application.UseCases.Construction.Create;
using ENGER.Application.UseCases.Construction.CreatePayment;
using ENGER.Application.UseCases.Construction.GetAll;
using ENGER.Application.UseCases.Construction.GetById;
using Microsoft.AspNetCore.Mvc;

namespace ENGER.API.Controllers
{
    [Route("api/obras")]
    [ApiController]
    public class ConstructionController : ControllerBase
    {
        public readonly CreateConstructionUseCase _createConstructionUseCase;
        public readonly GetByIdConstructionUseCase _getByIdConstructionUseCase;
        public readonly GetAllConstructionsUseCase _getAllConstructionUseCase;
        public readonly UpdateConstructionUseCase _updateConstructionUseCase;
        public readonly CreatePaymentUseCase _createPaymentConstructionUseCase;

        public ConstructionController(CreateConstructionUseCase createBudgetUseCase, GetByIdConstructionUseCase getByIdConstructionUseCase, 
            GetAllConstructionsUseCase getAllConstructionUseCase, UpdateConstructionUseCase updateConstructionUseCase, CreatePaymentUseCase createPaymentConstructionUseCase)
        {
            _createConstructionUseCase = createBudgetUseCase;
            _getByIdConstructionUseCase = getByIdConstructionUseCase;
            _getAllConstructionUseCase = getAllConstructionUseCase;
            _updateConstructionUseCase = updateConstructionUseCase;
            _createPaymentConstructionUseCase = createPaymentConstructionUseCase;
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

        [HttpGet("{constructionId}/{companyId}")]
        public async Task<IActionResult> GetById([FromRoute] int constructionId, [FromRoute] int companyId)
        {
            try
            {
                Domain.Entities.Construction objBudget = await _getByIdConstructionUseCase.ExecuteAsync(constructionId, companyId);
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

        [HttpGet("{companyId}")]
        public async Task<IActionResult> GetAll([FromRoute] int companyId)
        {
            try
            {
                IEnumerable<Domain.Entities.Construction> objBudget = await _getAllConstructionUseCase.ExecuteAsync(companyId);
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

        [HttpPut("{constructionId}/{companyId}")]
        public async Task<IActionResult> Update([FromRoute] int constructionId, [FromRoute] int companyId, [FromBody] ConstructionRequestDTO request)
        {
            try
            {
                Domain.Entities.Construction objBudget = await _updateConstructionUseCase.ExecuteAsync(request, constructionId, companyId);
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

        [HttpPut("pagamento/{constructionId}/{companyId}")]
        public async Task<IActionResult> Payment([FromRoute] int constructionId, [FromRoute] int companyId, [FromBody] ConstructionPaymentDTO request)
        {
            try
            {
                Domain.Entities.ConstructionPayment objPayment = await _createPaymentConstructionUseCase.ExecuteAsync(constructionId, companyId, request);
                return Ok(objPayment);
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
