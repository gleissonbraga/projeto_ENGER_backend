using ENGER.Application.DTOs.Company;
using ENGER.Application.DTOs.Employee;
using ENGER.Application.Exceptions;
using ENGER.Application.UseCases.Card.GetByIdCompany;
using ENGER.Application.UseCases.Client.Create;
using ENGER.Application.UseCases.Client.GetAll;
using ENGER.Application.UseCases.Client.GetById;
using ENGER.Application.UseCases.Employee.GetAll;
using ENGER.Application.UseCases.Employee.GetById;
using ENGER.Application.UseCases.Employee.Update;
using ENGER.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ENGER.API.Controllers
{
    [Route("api/cliente")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly CreateClientUseCase _createClientUseCase;
        private readonly UpdateClientUseCase _updateClientUseCase;
        private readonly GetByIdClientUseCase _getByIdClientUseCase;
        private readonly GetAllClientsByCompanyUseCase _getAllClientUseCase;

        public ClientController(CreateClientUseCase createClientUseCase, UpdateClientUseCase updateClientUseCase, GetByIdClientUseCase getByIdClientUseCase, GetAllClientsByCompanyUseCase getAllClientUseCase)
        {
            _createClientUseCase = createClientUseCase;
            _updateClientUseCase = updateClientUseCase;
            _getByIdClientUseCase = getByIdClientUseCase;
            _getAllClientUseCase = getAllClientUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromRoute] int companyId, [FromBody] ClientRequestDTO request)
        {
            try
            {
                ClientResponseDTO objClient = await _createClientUseCase.ExecuteAsync(request, companyId);
                return Ok(objClient);
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

        [HttpPut("{companyId}/{clientId}")]
        public async Task<IActionResult> Update([FromRoute] int companyId, [FromRoute] int clientId, [FromBody] ClientRequestDTO request)
        {
            try
            {
                Client objClient = await _updateClientUseCase.ExecuteAsync(companyId, clientId, request);
                return Ok(objClient);
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

        [HttpGet("{companyId}/{clientId}")]
        public async Task<IActionResult> GetById([FromRoute] int companyId, [FromRoute] int clientId)
        {
            try
            {
                Client objClient = await _getByIdClientUseCase.ExecuteAsync(companyId, clientId);
                return Ok(objClient);
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
        public async Task<IActionResult> GetAllByCompanyId([FromRoute] int companyId)
        {
            try
            {
                IEnumerable<Client> objClients = await _getAllClientUseCase.ExecuteAsync(companyId);
                return Ok(objClients);
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
