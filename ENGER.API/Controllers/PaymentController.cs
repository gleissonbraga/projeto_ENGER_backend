using ENGER.Application.DTOs.Company;
using ENGER.Application.Exceptions;
using ENGER.Application.UseCases.Card.GetByIdCompany;
using ENGER.Application.UseCases.Company.Create;
using ENGER.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ENGER.API.Controllers
{
    [Route("api/pagamentos")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly UpdatePaymentUseCase _updatePaymentUseCase;

        public PaymentController(UpdatePaymentUseCase updatePaymentUseCase)
        {
            _updatePaymentUseCase = updatePaymentUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> GetCardByIdCompany([FromBody] JsonElement payload)
        {
            try
            {
                await _updatePaymentUseCase.ExecuteAsync(payload);
                return Ok();
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
