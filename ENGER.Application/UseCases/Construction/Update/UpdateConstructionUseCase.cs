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
    public class UpdateConstructionUseCase
    {
        private readonly IConstructionRepository _repository;

        public UpdateConstructionUseCase(IConstructionRepository repository)
        {
            _repository = repository;
        }

        public async Task<Domain.Entities.Construction> ExecuteAsync(ConstructionRequestDTO request, int constructionId, int companyId)
        {
            var errors = new List<ValidationError>();

            // 1. VALIDAÇÕES BÁSICAS
            Validation.Validation.InputRequired(request.Description, "Description", errors);
            Validation.Validation.InputRequired(request.Status.ToString(), "Status", errors);

            if (errors.Count > 0)
                throw new ApplicException(errors);

            // 2. BUSCAR A OBRA EXISTENTE (Importante incluir as coleções para o EF comparar)
            var objConstruction = await _repository.GetByIdAsync(companyId, constructionId);

            if (objConstruction == null)
                throw new Exception("Obra não encontrada.");

            objConstruction.Description = request.Description;
            objConstruction.BudgetId = request.BudgetId;
            objConstruction.TotalPaidValue = request.TotalPaidValue ?? 0;
            objConstruction.TotalConstructionValue = request.TotalConstructionValue ?? 0;
            objConstruction.Street = request.Street;
            objConstruction.Number = request.Number;
            objConstruction.City = request.City;
            objConstruction.Neighborhood = request.Neighborhood;
            objConstruction.ZipCode = request.ZipCode;
            objConstruction.StateAbbreviation = request.StateAbbreviation;
            objConstruction.StateDescription = request.StateDescription;
            objConstruction.StartDate = request.StartDate;
            objConstruction.EstimatedDeliveryDate = request.EstimatedDeliveryDate;
            objConstruction.FinalizationDate = request.FinalizationDate;
            objConstruction.Status = request.Status;
            objConstruction.ResponsibleId = request.ResponsibleId;

            objConstruction.Stages.Clear();
            objConstruction.Employees.Clear();
            objConstruction.Rentals.Clear();

            // Re-adicionando conforme o Request
            request.Stages?.ForEach(s =>
                objConstruction.Stages.Add(new ConstructionStage(s.Description, s.Order, constructionId, s.Status)));

            request.Employees?.ForEach(e =>
                objConstruction.Employees.Add(new ConstructionEmployee(e.EmployeeId, constructionId)));

            request.Rentals?.ForEach(r =>
                objConstruction.Rentals.Add(new ConstructionRental(
                    r.EquipmentDescription, r.RentalValue, r.DaysCount, r.EntryDate, r.ExitDate, r.ReceivedBy, r.ReturnedBy, constructionId)));

            // 5. PERSISTÊNCIA
            await _repository.UpdateAsync(objConstruction);

            return objConstruction;
        }
    }
}
