using ENGER.Application.DTOs.Budget;
using ENGER.Application.DTOs.Company;
using ENGER.Application.Exceptions;
using ENGER.Domain.Enums;
using ENGER.Domain.Exceptions;
using ENGER.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENGER.Application.UseCases.Budget.GetAll
{
    public class GetAllBudgetUseCase
    {
        public readonly IBudgetRepository _repository;
        public readonly ICompanyRepository _companyRepository;
        public readonly IClientRepository _clientRepository;

        public GetAllBudgetUseCase(IBudgetRepository repository, ICompanyRepository companyRepository)
        {
            _repository = repository;
            _companyRepository = companyRepository;
        }

        public async Task<IEnumerable<BudgetResponseDTO>> ExecuteAsync(int companyId)
        {
            Domain.Entities.Company objCompany = await _companyRepository.GetByIdAsync(companyId);

            if (objCompany == null) throw new ApplicException("company", "Empresa não encontrada");

            IEnumerable<Domain.Entities.Budget> objBudgetResponse = await _repository.GetByBudgetCompanyAsync(companyId);

            IEnumerable<BudgetResponseDTO> objBudgetResponseDTO =
         objBudgetResponse.Select(budget => new BudgetResponseDTO(
             budget.BudgetId,
             budget.Description,
             budget.Status.ToString(),
             budget.CompanyId,

             budget.Client == null
                 ? null
                 : new ClientResponseDTO(
                     budget.Client.CompanyId,
                     budget.Client.ReasonName,
                     budget.Client.FantasyName,
                     budget.Client.RegistrationNumber,
                     budget.Client.RGIENumber,
                     budget.Client.Email,
                     budget.Client.Street,
                     budget.Client.Number,
                     budget.Client.City,
                     budget.Client.Neighborhood,
                     budget.Client.ZipCode,
                     budget.Client.FederativeUnit,
                     budget.Client.PhoneNumber,
                     budget.Client.CellNumber
                 ),

             budget.UserId,
             budget.TotalStepsValue,
             budget.TotalMaterialsValue,
             budget.TotalValue,
             budget.Observation,
             budget.EntryDate,
             budget.UpdatedAt,

             budget.Stages.Select(stage => new BudgetStageResponseDTO(
                 stage.StageId,
                 stage.Description,
                 stage.Order,

                 stage.Materials.Select(material => new BudgetMaterialResponseDTO(
                     material.BudgetMaterialId,
                     material.Description,
                     material.Unit,
                     material.PlannedQuantity,
                     material.UnitCost,
                     material.IsClientProvided
                 )),

                 stage.Labors.Select(labor => new BudgetLaborResponseDTO(
                     labor.BudgetLaborId,
                     labor.RoleId,
                     labor.PlannedHours,
                     labor.HourlyRate,
                     labor.SocialCharges
                 ))
             ))
         ));

            return objBudgetResponseDTO;
        }
    }
}