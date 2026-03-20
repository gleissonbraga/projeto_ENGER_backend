using ENGER.Application.DTOs.Budget;
using ENGER.Application.Exceptions;
using ENGER.Domain.Exceptions;
using ENGER.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENGER.Application.UseCases.Budget.GetByIdCompany
{
    public class CreateBudgetUseCase
    {
        public readonly IBudgetRepository _repository;

        public CreateBudgetUseCase(IBudgetRepository repository)
        {
            _repository = repository;
        }

        public async Task<Domain.Entities.Budget> ExecuteAsync(BudgetResponseDTO request, int companyId) 
        {
            //int? CompanyId,
            //int? ClientId,
            //string? Description,
            //decimal? TotalStepsValue,
            //decimal? TotalMaterialsValue,
            //decimal? TotalValue,
            //string? Notes,
            //DateTime? EntryDate,
            //DateTime? UpdatedAt

            Domain.Entities.Budget teste = null;

            var errors = new List<ValidationError>();

            return teste;
        }
    }
}
