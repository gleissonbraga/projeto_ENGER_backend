using ENGER.Application.DTOs.Employee;
using ENGER.Application.Exceptions;
using ENGER.Application.UseCases.Card.GetByIdCompany;
using ENGER.Application.UseCases.Employee.Create;
using ENGER.Application.UseCases.Employee.GetAll;
using ENGER.Application.UseCases.Employee.GetById;
using ENGER.Application.UseCases.Employee.Update;
using ENGER.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ENGER.API.Controllers
{
    [Route("api/funcionarios")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly CreateEmployeeUseCase _createEmployeeCardUseCase;
        private readonly UpdateEmployeeUseCase _updateEmployeeUseCase;
        private readonly GetByIdEmployeeUseCase _getByIdEmployeeUseCase;
        private readonly GetAllEmployeesByCompanyUseCase _getAllEmployeesByCompanyUseCase;

        public EmployeeController(CreateEmployeeUseCase createEmployeeCardUseCase, UpdateEmployeeUseCase updateEmployeeUseCase,
            GetByIdEmployeeUseCase getByIdEmployeeUseCase, GetAllEmployeesByCompanyUseCase getAllEmployeesByCompanyUseCase)
        {
            _createEmployeeCardUseCase = createEmployeeCardUseCase;
            _updateEmployeeUseCase = updateEmployeeUseCase;
            _getByIdEmployeeUseCase = getByIdEmployeeUseCase;
            _getAllEmployeesByCompanyUseCase = getAllEmployeesByCompanyUseCase;

        }

        [HttpPost("{companyId}")]
        public async Task<IActionResult> Create([FromRoute] int companyId, [FromBody] EmployeeRequestDTO request)
        {
            try
            {
                Employee objEmployee = await _createEmployeeCardUseCase.ExecuteAsync(companyId, request);
                return Ok(objEmployee);
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

        [HttpPut("{companyId}/{employeeId}")]
        public async Task<IActionResult> Update([FromRoute] int companyId, [FromRoute] int employeeId, [FromBody] EmployeeRequestDTO request)
        {
            try
            {
                Employee objEmployee = await _updateEmployeeUseCase.ExecuteAsync(companyId, employeeId, request);
                return Ok(objEmployee);
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

        [HttpGet("{companyId}/{employeeId}")]
        public async Task<IActionResult> GetById([FromRoute] int companyId, [FromRoute] int employeeId)
        {
            try
            {
                Employee objEmployee = await _getByIdEmployeeUseCase.ExecuteAsync(companyId, employeeId);
                return Ok(objEmployee);
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
                IEnumerable<Employee> objEmployees = await _getAllEmployeesByCompanyUseCase.ExecuteAsync(companyId);
                return Ok(objEmployees);
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
