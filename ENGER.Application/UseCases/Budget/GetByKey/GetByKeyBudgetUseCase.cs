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

namespace ENGER.Application.UseCases.Budget.GetByID
{
    public class GetByKeyBudgetUseCase
    {
        public readonly IBudgetRepository _repository;
        public readonly ICompanyRepository _companyRepository;
        public readonly IClientRepository _clientRepository;

        public GetByKeyBudgetUseCase(IBudgetRepository repository, ICompanyRepository companyRepository)
        {
            _repository = repository;
            _companyRepository = companyRepository;
        }

        public async Task<BudgetResponseDTO> ExecuteAsync(Guid keybudget, int companyId)
        {
            Domain.Entities.Company objCompany = await _companyRepository.GetByIdAsync(companyId);

            if (objCompany == null) throw new ApplicException("company", "Empresa não encontrada");

            Domain.Entities.Budget objBudgetResponse = await _repository.GetByKeyAsync(keybudget, companyId);

            if (objCompany == null) throw new ApplicException("Orçamento não encontrado", "Orçamento não encontrado");

            BudgetResponseDTO objBudgetResponseDTO = new BudgetResponseDTO(
                objBudgetResponse.BudgetId,
                objBudgetResponse.Description,
                objBudgetResponse.Status.ToString(),
                objBudgetResponse.CompanyId,
                objBudgetResponse.Client == null
                    ? null
                    : new ClientResponseDTO(
                        objBudgetResponse.Client.CompanyId,
                        objBudgetResponse.Client.ReasonName,
                        objBudgetResponse.Client.FantasyName,
                        objBudgetResponse.Client.RegistrationNumber,
                        objBudgetResponse.Client.RGIENumber,
                        objBudgetResponse.Client.Email,
                        objBudgetResponse.Client.Street,
                        objBudgetResponse.Client.Number,
                        objBudgetResponse.Client.City,
                        objBudgetResponse.Client.Neighborhood,
                        objBudgetResponse.Client.ZipCode,
                        objBudgetResponse.Client.FederativeUnit,
                        objBudgetResponse.Client.PhoneNumber,
                        objBudgetResponse.Client.CellNumber
                    ),

                objBudgetResponse.UserId,
                objBudgetResponse.TotalStepsValue,
                objBudgetResponse.TotalMaterialsValue,
                objBudgetResponse.TotalValue,
                objBudgetResponse.Observation,
                objBudgetResponse.EntryDate,
                objBudgetResponse.UpdatedAt,

                objBudgetResponse.Stages.Select(stage => new BudgetStageResponseDTO(
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
                    )).ToList(),

                    stage.Labors.Select(labor => new BudgetLaborResponseDTO(
                        labor.BudgetLaborId,
                        labor.RoleId,
                        labor.PlannedHours,
                        labor.HourlyRate,
                        labor.SocialCharges
                    )).ToList()
                )).ToList()
            );

            return objBudgetResponseDTO;
        }
    }
}