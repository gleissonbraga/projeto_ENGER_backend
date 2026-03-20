using ENGER.Application.DTOs.SubsciptionType;
using ENGER.Application.DTOs.User;
using ENGER.Application.Exceptions;
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
    [Route("api/tipo_assinatura")]
    [ApiController]
    public class SubscriptionTypeController : ControllerBase
    {
        private readonly CreateSubscriptionTypeUsecase _createSubscriptionTypeUseCase;
        private readonly UpdateTypeUseCase _updateSubscriptionTypeUseCase;
        private readonly DeleteTypeUseCase _deleteSubscriptionTypeUseCase;
        private readonly GetAllTypesUseCase _getAllSubscriptionTypeUseCase;
        private readonly GetByIdTypeUseCase _getByIdSubscriptionTypeUseCase;

        public SubscriptionTypeController(CreateSubscriptionTypeUsecase createCompanyUseCase, UpdateTypeUseCase updateSubscriptionTypeUseCase, DeleteTypeUseCase deleteSubscriptionTypeUseCase,
                GetAllTypesUseCase getAllSubscriptionTypeUseCase, GetByIdTypeUseCase getByIdSubscriptionTypeUseCase)
        {
            _createSubscriptionTypeUseCase = createCompanyUseCase;
            _updateSubscriptionTypeUseCase = updateSubscriptionTypeUseCase;
            _deleteSubscriptionTypeUseCase = deleteSubscriptionTypeUseCase;
            _getAllSubscriptionTypeUseCase = getAllSubscriptionTypeUseCase;
            _getByIdSubscriptionTypeUseCase = getByIdSubscriptionTypeUseCase;

        }

        [HttpPost("cadastro")]
        public async Task<IActionResult> Create([FromBody] SubscriptionTypeRequestDTO request)
        {
            try
            {
                var sub = await _createSubscriptionTypeUseCase.ExecuteAsync(request);
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

        [HttpPut("atualizar/{typeId}")]
        public async Task<IActionResult> Update([FromBody] SubscriptionTypeRequestDTO request, [FromRoute] int typeId)
        {
            try
            {
                var sub = await _updateSubscriptionTypeUseCase.ExecuteAsync(request, typeId);
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

        [HttpDelete("{typeId}")]
        public async Task<IActionResult> Delete([FromRoute] int typeId)
        {
            try
            {
                await _deleteSubscriptionTypeUseCase.ExecuteAsync(typeId);
                return Ok(new {message = "Tipo de Assinatura excluída."});
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

        [HttpGet("{typeId}")]
        public async Task<IActionResult> GetbyId([FromRoute] int typeId)
        {
            try
            {
                var sub = await _getByIdSubscriptionTypeUseCase.ExecuteAsync(typeId);
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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var subs = await _getAllSubscriptionTypeUseCase.ExecuteAsync();

            return Ok(subs);
        }
    }
}
