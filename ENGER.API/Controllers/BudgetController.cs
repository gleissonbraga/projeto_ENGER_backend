using ENGER.Application.DTOs.Budget;
using ENGER.Application.DTOs.Company;
using ENGER.Application.Exceptions;
using ENGER.Application.UseCases.Budget.Create;
using ENGER.Application.UseCases.Budget.GetAll;
using ENGER.Application.UseCases.Budget.GetByID;
using ENGER.Application.UseCases.Budget.Update;
using ENGER.Application.UseCases.Client.Create;
using ENGER.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ENGER.API.Controllers
{
    [Route("api/orcamento")]
    [ApiController]
    public class BudgetController : ControllerBase
    {
        public readonly CreateBudgetUseCase _createBudgetUseCase;
        public readonly UpdateBudgetUseCase _updateBudgetUseCase;
        public readonly GetByIdBudgetUseCase _getbyIdBudgetUseCase;
        public readonly GetAllBudgetUseCase _getAllBudgetUseCase;
        public readonly GetByKeyBudgetUseCase _getByKeyBudgetUseCase;

        public BudgetController(CreateBudgetUseCase createBudgetUseCase, GetByIdBudgetUseCase getByIdBudgetUseCase, GetAllBudgetUseCase getAllBudgetUseCase, UpdateBudgetUseCase updateBudgetUseCase, GetByKeyBudgetUseCase getByKeyBudgetUseCase)
        {
            _createBudgetUseCase = createBudgetUseCase;
            _getbyIdBudgetUseCase = getByIdBudgetUseCase;
            _getAllBudgetUseCase = getAllBudgetUseCase;
            _updateBudgetUseCase = updateBudgetUseCase;
            _getByKeyBudgetUseCase = getByKeyBudgetUseCase;
        }


        [HttpPost("{companyId}")]
        public async Task<IActionResult> Create([FromBody] BudgetRequestDTO request, [FromRoute] int companyId)
        {
            try
            {
                BudgetResponseDTO objBudget = await _createBudgetUseCase.ExecuteAsync(request, companyId);
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

        [HttpPut("{companyId}/{budgetId}")]
        public async Task<IActionResult> Update([FromBody] BudgetRequestDTO request, [FromRoute] int companyId, [FromRoute] int budgetId)
        {
            try
            {
                BudgetResponseDTO objBudget = await _updateBudgetUseCase.ExecuteAsync(request, budgetId, companyId);
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


        [HttpGet("{companyId}/{budgetId}")]
        public async Task<IActionResult> GetById( [FromRoute] int companyId, [FromRoute] int budgetId)
        {
            try
            {
                BudgetResponseDTO objBudget = await _getbyIdBudgetUseCase.ExecuteAsync(budgetId, companyId);
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

        [HttpGet("proposta/{companyId}/{key}")]
        public async Task<IActionResult> GetByKey([FromRoute] Guid key, [FromRoute] int companyId)
        {
            try
            {
                BudgetResponseDTO objBudget = await _getByKeyBudgetUseCase.ExecuteAsync(key, companyId);
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
                IEnumerable<BudgetResponseDTO> objBudgets = await _getAllBudgetUseCase.ExecuteAsync(companyId);
                return Ok(objBudgets);
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
