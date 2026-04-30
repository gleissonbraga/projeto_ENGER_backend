using ENGER.Application.DTOs.Construction;
using ENGER.Application.DTOs.Employee;
using ENGER.Application.DTOs.Position;
using ENGER.Application.DTOs.Subsciption;
using ENGER.Application.Exceptions;
using ENGER.Domain.Entities;
using ENGER.Domain.Enums;
using ENGER.Domain.Exceptions;
using ENGER.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ENGER.Application.UseCases.Construction.Create
{
    public class CreateConstructionUseCase
    {
        private readonly IConstructionRepository _repository;
        private readonly IBudgetRepository _budgetRepository;

        public CreateConstructionUseCase(IConstructionRepository repository, IBudgetRepository repositoryBudget)
        {
            _repository = repository;
            _budgetRepository = repositoryBudget;
        }

        public async Task<Domain.Entities.Construction> ExecuteAsync(Guid approvalToken, int companyId)
        {
            var errors = new List<ValidationError>();

            var objBudget = await _budgetRepository.GetByKeyAsync(approvalToken, companyId);

            if (objBudget == null)
            {
                errors.Add(new ValidationError("Budget", "Orçamento não encontrado ou link expirado."));
                throw new ApplicException(errors);
            }

            if (objBudget.Status == Status.BudApproved)
            {
                errors.Add(new ValidationError("Budget", "Este orçamento já foi aprovado anteriormente."));
                throw new ApplicException(errors);
            }

            var objConstruction = new Domain.Entities.Construction(
                objBudget.Description,
                objBudget.BudgetId,
                0,
                objBudget.TotalValue,
                objBudget.Street,       
                objBudget.Number,       
                objBudget.City,         
                objBudget.Neighborhood, 
                objBudget.ZipCode,      
                objBudget.StateAbbreviation,
                objBudget.StateDescription,
                DateTime.UtcNow,
                null,
                null, 
                1,
                companyId,
                objBudget.UserId
            );

            foreach (var budgetStage in objBudget.Stages)
            {
                var obraStage = new ConstructionStage(
                    budgetStage.Description,
                    budgetStage.Order,
                    0, 
                    1 
                );

                objConstruction.Stages.Add(obraStage);
            }

            objBudget.Status = (Status)Status.BudApproved;

            await _repository.AddAsync(objConstruction);
            await _budgetRepository.UpdateAsync(objBudget);

            return objConstruction;
        }

        public async Task<Domain.Entities.Construction> ExecuteAsyncOld(ConstructionRequestDTO request, int companyId)
        {
            var errors = new List<ValidationError>();

            // --- 1. VALIDAÇÕES DA OBRA (PAI) ---
            Validation.Validation.InputRequired(request.Description, "Description", errors);
            Validation.Validation.InputRequired(request.CompanyId.ToString(), "CompanyId", errors);
            Validation.Validation.InputRequired(request.Status.ToString(), "Status", errors);

            // --- 2. VALIDAÇÕES DOS FILHOS (CASO EXISTAM) ---

            // Validar Etapas (Stages)
            if (request.Stages != null && request.Stages.Any())
            {
                for (int i = 0; i < request.Stages.Count; i++)
                {
                    var stage = request.Stages[i];
                    Validation.Validation.InputRequired(stage.Description, $"Stages[{i}].Description", errors);
                    Validation.Validation.InputRequired(stage.Order.ToString(), $"Stages[{i}].Order", errors);
                }
            }

            // Validar Funcionários (Employees)
            if (request.Employees != null && request.Employees.Any())
            {
                for (int i = 0; i < request.Employees.Count; i++)
                {
                    var emp = request.Employees[i];
                    Validation.Validation.InputRequired(emp.EmployeeId.ToString(), $"Employees[{i}].EmployeeId", errors);
                }
            }

            // Validar Pagamentos (Payments)
            if (request.Payments != null && request.Payments.Any())
            {
                for (int i = 0; i < request.Payments.Count; i++)
                {
                    var pay = request.Payments[i];
                    Validation.Validation.InputRequired(pay.PaymentValue.ToString(), $"Payments[{i}].PaymentValue", errors);
                    if (pay.PaymentValue <= 0)
                        errors.Add(new ValidationError($"Payments[{i}].PaymentValue", "O valor do pagamento deve ser maior que zero."));
                }
            }

            // --- 3. VERIFICAÇÃO DE ERROS ---
            if (errors.Count > 0)
                throw new ApplicException(errors);

            // --- 4. INSTANCIAÇÃO E MAPEAMENTO ---
            var objConstruction = new Domain.Entities.Construction(
                request.Description,
                request.BudgetId,
                request.TotalPaidValue ?? 0,
                request.TotalConstructionValue ?? 0,
                request.Street,
                request.Number,
                request.City,
                request.Neighborhood,
                request.ZipCode,
                request.StateAbbreviation,
                request.StateDescription,
                request.StartDate,
                request.EstimatedDeliveryDate,
                request.FinalizationDate,
                request.Status,
                companyId,
                request.ResponsibleId
            );

            // Adição em cascata (O EF gerencia os vínculos automaticamente)
            request.Stages?.ForEach(s =>
                objConstruction.Stages.Add(new ConstructionStage(s.Description, s.Order, 0, s.Status)));

            request.Employees?.ForEach(e =>
                objConstruction.Employees.Add(new ConstructionEmployee(e.EmployeeId, 0)));

            request.Rentals?.ForEach(r =>
                objConstruction.Rentals.Add(new ConstructionRental(
                    r.EquipmentDescription, r.RentalValue, r.DaysCount, r.EntryDate, r.ExitDate, r.ReceivedBy, r.ReturnedBy, 0)));

            request.Attachments?.ForEach(a =>
                objConstruction.Attachments.Add(new ConstructionAttachment(a.Description, a.ImageUrl, 0)));

            request.Payments?.ForEach(p =>
                objConstruction.Payments.Add(new ConstructionPayment(p.PaymentDate, p.PaymentTypeId, 0, p.StageId, p.PaymentValue)));

            // --- 5. PERSISTÊNCIA ---
            await _repository.AddAsync(objConstruction);

            return objConstruction;
        }
    }
}
