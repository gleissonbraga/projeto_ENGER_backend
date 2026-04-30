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
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace ENGER.Application.UseCases.Budget.Create
{
    public class CreateBudgetUseCase
    {
        private readonly IBudgetRepository _repository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IEmailService _emailService;


        public CreateBudgetUseCase(IBudgetRepository repository, ICompanyRepository companyRepository, IClientRepository clientRepository, IEmailService emailService)
        {
            _repository = repository;
            _companyRepository = companyRepository;
            _clientRepository = clientRepository;
            _emailService = emailService;
        }

        public async Task<BudgetResponseDTO> ExecuteAsync(BudgetRequestDTO request, int companyId)
        {
            var errors = new List<ValidationError>();

            Validation.Validation.InputRequired(request.description, "description", errors);
            Validation.Validation.MaxLength(request.description, 255, "description", errors);

            Validation.Validation.InputRequired(request.observation, "observation", errors);
            Validation.Validation.MaxLength(request.observation, 255, "observation", errors);

            Validation.Validation.InputRequired(request.street, "street", errors);
            Validation.Validation.InputRequired(request.city, "city", errors);
            Validation.Validation.InputRequired(request.neighborhood, "neighborhood", errors);
            Validation.Validation.InputRequired(request.zipCode, "zipCode", errors);
            Validation.Validation.InputRequired(request.stateAbbreviation, "stateAbbreviation", errors);
            Validation.Validation.MaxLength(request.stateAbbreviation, 2, "stateAbbreviation", errors);

            Validation.Validation.InputRequired(request.totalStepsValue.ToString(), "totalStepsValue", errors);
            Validation.Validation.IsDecimal(request.totalStepsValue.ToString(), "totalStepsValue", errors);

            Validation.Validation.InputRequired(request.totalMaterialsValue.ToString(), "totalMaterialsValue", errors);
            Validation.Validation.IsDecimal(request.totalMaterialsValue.ToString(), "totalMaterialsValue", errors);

            Validation.Validation.InputRequired(request.totalValue.ToString(), "totalMaterialsValue", errors);
            Validation.Validation.IsDecimal(request.totalValue.ToString(), "totalMaterialsValue", errors);

            if (request.Stages == null || !request.Stages.Any())
            {
                errors.Add(new ValidationError("stages", "At least one stage is required"));
            }
            else
            {
                for (int i = 0; i < request.Stages.Count; i++)
                {
                    var stage = request.Stages[i];

                    // Stage
                    Validation.Validation.InputRequired(stage.description, $"stages[{i}].description", errors);
                    Validation.Validation.MaxLength(stage.description, 255, $"stages[{i}].description", errors);

                    // Materials
                    if (stage.Materials != null)
                    {
                        for (int j = 0; j < stage.Materials.Count; j++)
                        {
                            var material = stage.Materials[j];

                            Validation.Validation.InputRequired(material.Description, $"description", errors);
                            Validation.Validation.MaxLength(material.Description, 255, $"description", errors);

                            Validation.Validation.InputRequired(material.Unit, $"unit", errors);
                            Validation.Validation.MaxLength(material.Unit, 50, $"unit", errors);

                            Validation.Validation.IsDecimal(material.PlannedQuantity.ToString(), $"plannedQuantity", errors);
                            Validation.Validation.IsDecimal(material.UnitCost.ToString(), $"unitCost", errors);
                        }
                    }

                    // Labors
                    if (stage.Labors != null)
                    {
                        for (int k = 0; k < stage.Labors.Count; k++)
                        {
                            var labor = stage.Labors[k];

                            Validation.Validation.InputRequired(labor.RoleId.ToString(), $"roleId", errors);

                            Validation.Validation.IsDecimal(labor.PlannedHours.ToString(), $"plannedHours", errors);
                            Validation.Validation.IsDecimal(labor.HourlyRate.ToString(), $"hourlyRate", errors);
                            Validation.Validation.IsDecimal(labor.SocialCharges.ToString(), $"socialCharges", errors);
                        }
                    }
                }
            }

            Domain.Entities.Company objCompany = await _companyRepository.GetByIdAsync(companyId);

            if (objCompany == null)
                errors.Add(new ValidationError("company", "Empresa não encontrada"));


            Domain.Entities.Client objCClient = await _clientRepository.GetByIdAsync(companyId, (int)request.clientId);

            if (objCClient == null)
                errors.Add(new ValidationError("client", "Cliente não encontrada"));

            if (errors.Any())
                throw new ApplicException(errors);

            var stages = request.Stages.Select(stageDto =>
            {
                var stage = new Domain.Entities.BudgetStage(
                    stageDto.description,
                    stageDto.order,
                    Status.SubInProcess
                );

                // Materials
                if (stageDto.Materials != null)
                {
                    foreach (var materialDto in stageDto.Materials)
                    {
                        stage.Materials.Add(new Domain.Entities.BudgetMaterial(
                            materialDto.Description,
                            materialDto.Unit,
                            materialDto.PlannedQuantity,
                            materialDto.UnitCost,
                            materialDto.IsClientProvided
                        ));
                    }
                }

                // Labors
                if (stageDto.Labors != null)
                {
                    foreach (var laborDto in stageDto.Labors)
                    {
                        stage.Labors.Add(new Domain.Entities.BudgetLabor(
                            laborDto.RoleId,
                            laborDto.PlannedHours,
                            laborDto.HourlyRate,
                            laborDto.SocialCharges
                        ));
                    }
                }

                return stage;
            }).ToList();

            Domain.Entities.Budget objBudget = new Domain.Entities.Budget(
                 companyId,
                 request.clientId,
                 request.userId,
                 request.description,
                 Status.BudPending,
                 request.totalStepsValue,
                 request.totalMaterialsValue,
                 request.totalValue,
                 request.observation,
                 Guid.NewGuid(),
                 request.street,
                 request.number,
                 request.city,
                 request.neighborhood,
                 request.zipCode,
                 request.stateAbbreviation,
                 request.stateDescription
             );

            foreach (var stage in stages)
            {
                objBudget.Stages.Add(stage);
            }

            Domain.Entities.Budget objBudgetResponse = await _repository.AddAsync(objBudget);

            var response = new BudgetResponseDTO(
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

            string emailBody = $@"
                <div style='font-family: sans-serif; max-width: 600px;'>
                    <h2 style='color: #ff6600;'>Olá, {objCClient.FantasyName}!</h2>
                    <p>Seu orçamento para <strong>{request.description}</strong> está pronto.</p>
                    <p><strong>Valor Total:</strong> R$ {request.totalValue:N2}</p>
                    <p>O PDF detalhado segue em anexo para sua análise.</p>
                    <br>
                    <a href='https://seu-sistema.com/aprovar/{objBudgetResponse.KeyBudget}' 
                       style='background-color: #333; color: white; padding: 10px 20px; text-decoration: none; border-radius: 5px;'>
                       Visualizar e Aprovar Orçamento
                    </a>
                </div>";

            // 2. Aqui você geraria os bytes do PDF (usando QuestPDF ou similar)
            byte[] pdfContent = GerarPdfBytes(objBudgetResponse); // Método que você vai criar

            // 3. Envia o e-mail completo
            await _emailService.SendEmailAsync(
                "bragagleisson@gmail.com",
                "Orçamento Disponível - ENGER",
                emailBody,
                pdfContent,
                "Orcamento.pdf"
            );

            return response;
        }

        public byte[] GerarPdfBytes(Domain.Entities.Budget budget)
        {
            // QuestPDF precisa da licença comunitária para uso gratuito
            QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;

            return QuestPDF.Fluent.Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(50);
                    page.Header().Text($"Orçamento: {budget.Description}").FontSize(20).FontColor("#ff6600");

                    page.Content().Column(col => {
                        col.Item().Text($"Cliente: {budget.Client?.FantasyName}");
                        col.Item().Text($"Valor Total: R$ {budget.TotalValue:N2}");
                        col.Item().PaddingTop(10).Text("Endereço da Obra:").Bold();
                        col.Item().Text($"{budget.Street}, {budget.Number} - {budget.City}/{budget.StateAbbreviation}");
                    });
                });
            }).GeneratePdf();
        }
    }
}
