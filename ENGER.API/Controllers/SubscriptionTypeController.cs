using ENGER.Application.DTOs.SubsciptionType;
using ENGER.Application.UseCases.SubscriptionType.Create;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ENGER.API.Controllers
{
    [Route("api/tipo_assinatura")]
    [ApiController]
    public class SubscriptionTypeController : ControllerBase
    {
        private readonly CreateSubscriptionTypeUsecase _createSubscriptionTypeUseCase;

        public SubscriptionTypeController(CreateSubscriptionTypeUsecase createCompanyUseCase)
        {
            _createSubscriptionTypeUseCase = createCompanyUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSubscriptionTypeRequest request)
        {
            // Chama o Use Case
            var id = await _createSubscriptionTypeUseCase.ExecuteAsync(request);

            // Retorna 201 Created com o ID gerado
            return CreatedAtAction(nameof(Create), new { id = id }, id);
        }
    }
}
