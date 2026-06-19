using ENGER.Application.DTOs.Position;
using ENGER.Application.Exceptions;
using ENGER.Application.UseCases.Card.GetByIdCompany;
using ENGER.Application.UseCases.Position.Create;
using ENGER.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ENGER.API.Controllers
{
    [Route("api/cargos")]
    [ApiController]
    public class PositionController : ControllerBase
    {
        public readonly CreatePositionUseCase _createPositionUseCase;
        public readonly UpdatePositionUseCase _updatePositionUseCase;
        public readonly GetByIdPositionUseCase _getByIdPositionUseCase;
        public readonly GetAllPositionByCompanyUseCase _getAllPositionByCompanyUseCase;

        public PositionController(CreatePositionUseCase createPositionUseCase, UpdatePositionUseCase updatePositionUseCase, GetByIdPositionUseCase getByIdPositionUseCase, GetAllPositionByCompanyUseCase getAllPositionByCompanyUseCase)
        {
            _createPositionUseCase = createPositionUseCase;
            _updatePositionUseCase = updatePositionUseCase;
            _getByIdPositionUseCase = getByIdPositionUseCase;
            _getAllPositionByCompanyUseCase = getAllPositionByCompanyUseCase;
        }


        [HttpPost("{companyId}")]
        public async Task<IActionResult> Create([FromRoute] int companyId, [FromQuery] PositionRequestDTO request) 
        {
            try
            {
                Position objPosition = await _createPositionUseCase.ExecuteAsync(companyId, request);
                return Ok(objPosition);
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

        [HttpPut("{companyId}/{positionId}")]
        public async Task<IActionResult> Update([FromRoute] int companyId, [FromRoute] int positionId, [FromQuery] PositionRequestDTO request)
        {
            try
            {
                Position objPosition = await _updatePositionUseCase.ExecuteAsync(companyId, positionId, request);
                return Ok(objPosition);
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

        [HttpGet("{companyId}/{positionId}")]
        public async Task<IActionResult> GetById([FromRoute] int companyId, [FromRoute] int positionId)
        {
            try
            {
                Position objPosition = await _getByIdPositionUseCase.ExecuteAsync(companyId, positionId);
                return Ok(objPosition);
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
        public async Task<IActionResult> GetAllCompany([FromRoute] int companyId)
        {
            try
            {
                IEnumerable<Position> objPositions = await _getAllPositionByCompanyUseCase.ExecuteAsync(companyId);
                return Ok(objPositions);
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
