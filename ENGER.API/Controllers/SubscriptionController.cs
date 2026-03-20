using ENGER.Application.DTOs.Subsciption;
using ENGER.Application.DTOs.SubsciptionType;
using ENGER.Application.DTOs.User;
using ENGER.Application.Exceptions;
using ENGER.Application.UseCases.Subscription.Create;
using ENGER.Application.UseCases.SubscriptionType.Create;
using ENGER.Application.UseCases.User.Delete;
using ENGER.Application.UseCases.User.GetAll;
using ENGER.Application.UseCases.User.GetById;
using ENGER.Application.UseCases.User.Update;
using ENGER.Application.UseCases.User.UpdateUser;
using ENGER.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ENGER.API.Controllers
{
    [Route("api/assinatura")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly CreateSubscriptionUseCase _createSubscriptionUseCase;

        public SubscriptionController(CreateSubscriptionUseCase createSubscriptionUseCase)
        {
            _createSubscriptionUseCase = createSubscriptionUseCase;


        }

        [HttpPost("{companyId}")]
        public async Task<IActionResult> Create([FromRoute] int companyId, [FromBody] SubscriptionRequestDTO request)
        {
            try
            {
                var sub = await _createSubscriptionUseCase.ExecuteAsync(companyId, request);
                return Ok(sub);
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
