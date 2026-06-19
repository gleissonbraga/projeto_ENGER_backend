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

        //public async Task<Domain.Entities.Construction> ExecuteAsync(ConstructionRequestDTO request, int constructionId, int companyId)
        //{
        //    var errors = new List<ValidationError>();

        //    // 1. VALIDAÇÕES BÁSICAS
        //    Validation.Validation.InputRequired(request.Description, "Description", errors);
        //    Validation.Validation.InputRequired(request.Status.ToString(), "Status", errors);

        //    if (errors.Count > 0)
        //        throw new ApplicException(errors);

        //    // 2. BUSCAR A OBRA EXISTENTE (Importante incluir as coleções para o EF comparar)
        //    var objConstruction = await _repository.GetByIdAsync(companyId, constructionId);

        //    if (objConstruction == null)
        //        throw new Exception("Obra não encontrada.");

        //    objConstruction.Description = request.Description;
        //    objConstruction.BudgetId = request.BudgetId;
        //    objConstruction.TotalPaidValue = request.TotalPaidValue ?? 0;
        //    objConstruction.TotalConstructionValue = request.TotalConstructionValue ?? 0;
        //    objConstruction.Street = request.Street;
        //    objConstruction.Number = request.Number;
        //    objConstruction.City = request.City;
        //    objConstruction.Neighborhood = request.Neighborhood;
        //    objConstruction.ZipCode = request.ZipCode;
        //    objConstruction.StateAbbreviation = request.StateAbbreviation;
        //    objConstruction.StateDescription = request.StateDescription;
        //    objConstruction.StartDate = request.StartDate;
        //    objConstruction.EstimatedDeliveryDate = request.EstimatedDeliveryDate;
        //    objConstruction.FinalizationDate = request.FinalizationDate;
        //    objConstruction.Status = request.Status;
        //    objConstruction.ResponsibleId = request.ResponsibleId;

        //    objConstruction.Stages.Clear();
        //    objConstruction.Employees.Clear();
        //    objConstruction.Rentals.Clear();

        //    // Re-adicionando conforme o Request
        //    request.Stages?.ForEach(s =>
        //        objConstruction.Stages.Add(new ConstructionStage(s.Description, s.Order, constructionId, s.Status)));

        //    request.Employees?.ForEach(e =>
        //        objConstruction.Employees.Add(new ConstructionEmployee(e.EmployeeId, constructionId)));

        //    request.Attachments?.ForEach(e =>
        //        objConstruction.Attachments.Add(new ConstructionAttachment(e.Description, e.ImageUrl, constructionId)));

        //    request.Rentals?.ForEach(r =>
        //        objConstruction.Rentals.Add(new ConstructionRental(
        //            r.EquipmentDescription, r.RentalValue, r.DaysCount, r.EntryDate, r.ExitDate, r.ReceivedBy, r.ReturnedBy, constructionId)));

        //    // 5. PERSISTÊNCIA
        //    await _repository.UpdateAsync(objConstruction);

        //    return objConstruction;
        //}

        public async Task<Domain.Entities.Construction> ExecuteAsync(ConstructionRequestDTO request, int constructionId, int companyId)
        {
            var errors = new List<ValidationError>();

            // 1. VALIDAÇÕES BÁSICAS
            Validation.Validation.InputRequired(request.Description, "Description", errors);
            Validation.Validation.InputRequired(request.Status.ToString(), "Status", errors);

            if (errors.Count > 0)
                throw new ApplicException(errors);

            // 2. BUSCAR A OBRA EXISTENTE (Certifique-se de que o repositório tem os .Includes)
            var objConstruction = await _repository.GetByIdAsync(companyId, constructionId);

            if (objConstruction == null)
                throw new Exception("Obra não encontrada.");

            // 3. ATUALIZAÇÃO DOS DADOS PRINCIPAIS DA OBRA
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

            // ==========================================
            // 🚀 4. SINCRONIZAÇÃO DE ETAPAS (Stages)
            // ==========================================
            if (request.Stages != null)
            {
                // Remove os que não vieram no request
                var etapasParaRemover = objConstruction.Stages.Where(db => !request.Stages.Any(dto => dto.stageId == db.StageId)).ToList();
                foreach (var item in etapasParaRemover) objConstruction.Stages.Remove(item);

                foreach (var dto in request.Stages)
                {
                    var existente = objConstruction.Stages.FirstOrDefault(s => s.StageId == dto.stageId && dto.stageId > 0);
                    if (existente != null)
                    {
                        existente.Description = dto.Description;
                        existente.Order = dto.Order;
                        existente.Status = dto.Status;
                    }
                    else
                    {
                        objConstruction.Stages.Add(new ConstructionStage(dto.Description, dto.Order, constructionId, dto.Status));
                    }
                }
            }

            // ==========================================
            // 🚀 5. SINCRONIZAÇÃO DE FUNCIONÁRIOS (Employees)
            // ==========================================
            if (request.Employees != null)
            {
                // Aqui a chave de comparação geralmente é o EmployeeId
                var funcParaRemover = objConstruction.Employees.Where(db => !request.Employees.Any(dto => dto.EmployeeId == db.EmployeeId)).ToList();
                foreach (var item in funcParaRemover) objConstruction.Employees.Remove(item);

                foreach (var dto in request.Employees)
                {
                    // Se o funcionário ainda não está na lista desta obra, adicionamos
                    if (!objConstruction.Employees.Any(e => e.EmployeeId == dto.EmployeeId))
                    {
                        objConstruction.Employees.Add(new ConstructionEmployee(dto.EmployeeId, constructionId));
                    }
                    // Não precisa de "Update" aqui, pois é só uma tabela de ligação (Obra x Funcionário)
                }
            }

            // ==========================================
            // 🚀 6. SINCRONIZAÇÃO DE ANEXOS (Attachments)
            // ==========================================
            if (request.Attachments != null)
            {
                // Nota: Assumindo que o seu DTO tenha um AttachmentId. Se não tiver, você precisa adicionar!
                var anexosParaRemover = objConstruction.Attachments.Where(db => !request.Attachments.Any(dto => dto.attachmentId == db.ConstructionAttachmentId)).ToList();
                foreach (var item in anexosParaRemover) objConstruction.Attachments.Remove(item);

                foreach (var dto in request.Attachments)
                {
                    var existente = objConstruction.Attachments.FirstOrDefault(a => a.ConstructionAttachmentId == dto.attachmentId && dto.attachmentId > 0);
                    if (existente != null)
                    {
                        existente.Description = dto.Description;
                        existente.ImageUrl = dto.ImageUrl;
                    }
                    else
                    {
                        objConstruction.Attachments.Add(new ConstructionAttachment(dto.Description, dto.ImageUrl, constructionId));
                    }
                }
            }

            // ==========================================
            // 🚀 7. SINCRONIZAÇÃO DE ALUGUÉIS (Rentals)
            // ==========================================
            if (request.Rentals != null)
            {
                // Nota: Assumindo que o seu DTO tenha um RentalId.
                var alugueisParaRemover = objConstruction.Rentals.Where(db => !request.Rentals.Any(dto => dto.rentalId == db.RentalId)).ToList();
                foreach (var item in alugueisParaRemover) objConstruction.Rentals.Remove(item);

                foreach (var dto in request.Rentals)
                {
                    var existente = objConstruction.Rentals.FirstOrDefault(r => r.RentalId == dto.rentalId && dto.rentalId > 0);
                    if (existente != null)
                    {
                        existente.EquipmentDescription = dto.EquipmentDescription;
                        existente.RentalValue = dto.RentalValue;
                        existente.DaysCount = dto.DaysCount;
                        existente.EntryDate = dto.EntryDate;
                        existente.ExitDate = dto.ExitDate;
                        existente.ReceivedBy = dto.ReceivedBy;
                        existente.ReturnedBy = dto.ReturnedBy;
                    }
                    else
                    {
                        objConstruction.Rentals.Add(new ConstructionRental(
                            dto.EquipmentDescription, dto.RentalValue, dto.DaysCount, dto.EntryDate, dto.ExitDate, dto.ReceivedBy, dto.ReturnedBy, constructionId));
                    }
                }
            }

            // 8. PERSISTÊNCIA
            await _repository.UpdateAsync(objConstruction);

            return objConstruction;
        }
    }
}
