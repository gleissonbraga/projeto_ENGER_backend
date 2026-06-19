using ENGER.Application.DTOs.Company;
using ENGER.Application.Exceptions;
using ENGER.Application.UseCases.Card.GetByIdCompany;
using ENGER.Application.UseCases.Company.Create;
using ENGER.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ENGER.API.Controllers
{
    [Route("api/cartoes")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly GetCardUseCase _getCardUseCase;

        public CardController(GetCardUseCase getCardUseCase)
        {
            _getCardUseCase = getCardUseCase;
        }

        [HttpGet("{companyId}")]
        public async Task<IActionResult> GetCardByIdCompany([FromRoute] int companyId)
        {
            try
            {
                Card objCard = await _getCardUseCase.ExecuteAsync(companyId);
                return Ok(objCard);
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
