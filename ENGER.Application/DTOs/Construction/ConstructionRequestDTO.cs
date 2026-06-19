using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENGER.Application.DTOs.Construction
{
    public record ConstructionRequestDTO
    (
        string? Description,
        int? BudgetId,
        decimal? TotalPaidValue,
        decimal? TotalConstructionValue,

        // Endereço
        string? Street,
        string? Number,
        string? City,
        string? Neighborhood,
        string? ZipCode,
        string? StateAbbreviation,
        string? StateDescription,

        DateTime? StartDate,
        DateTime? EstimatedDeliveryDate,
        DateTime? FinalizationDate,

        int Status,
        int? CompanyId,
        int? ResponsibleId,

        // Listas para salvamento em cascata
        List<ConstructionStageDTO> Stages,
        List<ConstructionEmployeeDTO> Employees,
        List<ConstructionRentalDTO> Rentals,
        List<ConstructionAttachmentDTO> Attachments,
        List<ConstructionPaymentDTO> Payments
    );

        // Etapas da Obra
    public record ConstructionStageDTO(
        int? stageId,
        string? Description,
        int? Order,
        int? Status
    );

    // Funcionários vinculados à obra
    public record ConstructionEmployeeDTO(
        int? EmployeeId
    );

    // Aluguéis de equipamentos
    public record ConstructionRentalDTO(
        int? rentalId,
        string? EquipmentDescription,
        decimal? RentalValue,
        int? DaysCount,
        DateTime? EntryDate,
        DateTime? ExitDate,
        string? ReceivedBy,
        string? ReturnedBy
    );

    // Fotos e documentos iniciais
    public record ConstructionAttachmentDTO(
        int? attachmentId,
        string? Description,
        string? ImageUrl
    );

    // Pagamentos/Medições
    public record ConstructionPaymentDTO(
        DateTime? PaymentDate,
        int? PaymentTypeId,
        int? StageId,
        decimal? PaymentValue
    );
}
