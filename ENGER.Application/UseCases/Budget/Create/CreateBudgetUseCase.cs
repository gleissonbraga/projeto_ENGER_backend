using ENGER.Application.DTOs.Budget;
using ENGER.Application.DTOs.Company;
using ENGER.Application.Exceptions;
using ENGER.Domain.Entities;
using ENGER.Domain.Enums;
using ENGER.Domain.Exceptions;
using ENGER.Domain.Interfaces.Repositories;
using ENGER.Domain.Services;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENGER.Application.UseCases.Budget.Create
{
    public class CreateBudgetUseCase
    {
        private readonly IBudgetRepository _repository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IEmailService _emailService;
        private readonly IUserRepository _userRepository;
        private readonly IUserContext _userContext;


        public CreateBudgetUseCase(IBudgetRepository repository, ICompanyRepository companyRepository, IClientRepository clientRepository, IEmailService emailService, IUserRepository userRepository, IUserContext userContex)
        {
            _repository = repository;
            _companyRepository = companyRepository;
            _clientRepository = clientRepository;
            _emailService = emailService;
            _userRepository = userRepository;
            _userContext = userContex;
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


            Domain.Entities.Client objCClient = await _clientRepository.GetByIdAsync((int)request.clientId, companyId);

            if (objCClient == null)
                errors.Add(new ValidationError("client", "Cliente não encontrada"));

            var userId = _userContext.GetUserId();

            Domain.Entities.User objUser = await _userRepository.GetByIdAsync(companyId, userId);

            if (objUser == null)
                errors.Add(new ValidationError("user", "Usuário não encontrado"));

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

            string linkAceite = $"http://localhost:3000/proposta/{objBudgetResponse.KeyBudget}";

            string emailBody = $@"
                    <div style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; background-color: #ffffff; border: 1px solid #e0e0e0; border-radius: 8px; overflow: hidden;'>
    
                        <!-- Cabeçalho -->
                        <div style='background-color: #000000; padding: 25px; text-align: center;'>
                            <h1 style='color: #ff6600; margin: 0; font-size: 26px; letter-spacing: 1px;'>ENGER</h1>
                        </div>

                        <!-- Corpo -->
                        <div style='padding: 30px; color: #1a1a1a; line-height: 1.6;'>
                            <h2 style='color: #ff6600; margin-top: 0;'>Olá, {objBudgetResponse.Client?.FantasyName}!</h2>
        
                            <p>O orçamento para a obra <strong>{objBudgetResponse.Description}</strong> está pronto para sua análise.</p>
        
                            <p style='font-size: 16px;'><strong>Valor Total:</strong> R$ {objBudgetResponse.TotalValue:N2}</p>
        
                            <p>O PDF detalhado segue em anexo. Para iniciar o projeto e transformar este orçamento em uma obra ativa, clique no botão abaixo para revisar os termos e confirmar o aceite:</p>

                            <!-- Botão CTA (Call to Action) -->
                            <div style='text-align: center; margin: 35px 0;'>
                                <a href='{linkAceite}' 
                                   style='background-color: #ff6600; color: #ffffff; text-decoration: none; padding: 16px 32px; border-radius: 4px; font-weight: bold; display: inline-block; font-size: 16px; text-transform: uppercase;'>
                                   Analisar e Aprovar Orçamento
                                </a>
                            </div>

                            <!-- Fallback de Link -->
                            <p style='font-size: 13px; color: #666666; margin-bottom: 5px;'>Se o botão não funcionar, copie e cole o link abaixo no seu navegador:</p>
                            <p style='font-size: 12px; color: #4a90e2; word-break: break-all; margin-top: 0;'>
                                <a href='{linkAceite}' style='color: #ff6600;'>{linkAceite}</a>
                            </p>
                        </div>

                        <!-- Rodapé -->
                        <div style='background-color: #f8f9fa; padding: 20px; text-align: center; color: #888888; font-size: 12px; border-top: 1px solid #e0e0e0;'>
                            <p style='margin: 0;'>Este é um e-mail automático enviado pelo sistema ENGER.</p>
                            <p style='margin: 5px 0 0 0;'>&copy; {DateTime.Now.Year} ENGER - Gestão de Obras</p>
                        </div>
                    </div>";

            byte[] pdfContent = GerarPdfBytes(objBudgetResponse);

            var emailFila = new SendEmail(
                to: "bragagleisson@gmail.com",
                subject: "Orçamento Disponível - ENGER",
                body: emailBody,
                status: Status.EmailNotSent,
                recordDate: DateTime.UtcNow,
                fileName: "Orçamento.pdf",
                attachmentContent: pdfContent
            );

            await _emailService.RecordEmail(emailFila);
            return response;
        }

        public byte[] GerarPdfBytes(Domain.Entities.Budget budget)
        {
            // QuestPDF precisa da licença comunitária para uso gratuito
            QuestPDF.Settings.License = LicenseType.Community;

            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(40);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(10).FontFamily(Fonts.Arial));

                    // CABEÇALHO DO DOCUMENTO
                    page.Header().Row(row =>
                    {
                        row.RelativeItem().Column(col =>
                        {
                            col.Item().Text($"Orçamento: {budget.Description}")
                                .FontSize(20).SemiBold().FontColor("#ff6600");
                            col.Item().Text($"ID do Orçamento: {budget.BudgetId} | Status: {budget.Status}")
                                .FontSize(11).FontColor(Colors.Grey.Darken2);
                        });

                        row.ConstantItem(120).AlignRight().Column(col =>
                        {
                            col.Item().Text($"Data: {budget.EntryDate:dd/MM/yyyy}");
                            if (budget.UpdatedAt.HasValue)
                                col.Item().Text($"Atualizado: {budget.UpdatedAt.Value:dd/MM/yyyy}");
                        });
                    });

                    // CORPO DO DOCUMENTO
                    page.Content().PaddingVertical(15).Column(col =>
                    {
                        // 1. DADOS DO CLIENTE
                        if (budget.Client != null)
                        {
                            col.Item().Background(Colors.Grey.Lighten4).Padding(10).Column(c =>
                            {
                                c.Item().Text("Dados do Cliente").FontSize(14).SemiBold().FontColor(Colors.Black);
                                c.Item().PaddingBottom(5).LineHorizontal(1).LineColor(Colors.Grey.Lighten2);

                                c.Item().Text($"Nome/Fantasia: {budget.Client.FantasyName} ({budget.Client.ReasonName})");
                                c.Item().Text($"CNPJ/CPF: {budget.Client.RegistrationNumber} | IE/RG: {budget.Client.RGIENumber}");
                                c.Item().Text($"Endereço: {budget.Client.Street}, {budget.Client.Number} - {budget.Client.Neighborhood}");
                                c.Item().Text($"Cidade/UF: {budget.Client.City}/{budget.Client.FederativeUnit} - CEP: {budget.Client.ZipCode}");
                                c.Item().Text($"Contatos: {budget.Client.PhoneNumber} / {budget.Client.CellNumber} | E-mail: {budget.Client.Email}");
                            });
                        }

                        // 2. DETALHAMENTO DAS ETAPAS
                        col.Item().PaddingTop(20).Text("Detalhamento das Etapas").FontSize(16).SemiBold().FontColor("#ff6600");

                        if (budget.Stages != null && budget.Stages.Any())
                        {
                            foreach (var stage in budget.Stages.OrderBy(s => s.Order))
                            {
                                col.Item().PaddingTop(15).Background(Colors.Grey.Lighten3).Padding(5)
                                    .Text($"Etapa {stage.Order}: {stage.Description}").SemiBold().FontSize(12);

                                // Tabela de Materiais da Etapa
                                if (stage.Materials != null && stage.Materials.Any())
                                {
                                    col.Item().PaddingTop(5).Text("Materiais").SemiBold();
                                    col.Item().PaddingTop(2).Table(table =>
                                    {
                                        table.ColumnsDefinition(columns =>
                                        {
                                            columns.RelativeColumn(3); // Descrição
                                            columns.RelativeColumn(1); // Unidade
                                            columns.RelativeColumn(1); // Qtd
                                            columns.RelativeColumn(1); // Valor Unit.
                                            columns.RelativeColumn(1); // Subtotal
                                            columns.RelativeColumn(1); // Fornecido por
                                        });

                                        table.Header(header =>
                                        {
                                            header.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Text("Descrição").SemiBold();
                                            header.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Text("Unid.").SemiBold();
                                            header.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).AlignRight().Text("Qtd").SemiBold();
                                            header.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).AlignRight().Text("V. Unit").SemiBold();
                                            header.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).AlignRight().Text("Subtotal").SemiBold();
                                            header.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).AlignCenter().Text("Cliente Fornece?").SemiBold();
                                        });

                                        foreach (var mat in stage.Materials)
                                        {
                                            var subtotal = mat.PlannedQuantity * mat.UnitCost;
                                            var fornecidoCliente = mat.IsClientProvided ? "Sim" : "Não";

                                            table.Cell().Text(mat.Description);
                                            table.Cell().Text(mat.Unit);
                                            table.Cell().AlignRight().Text(mat.PlannedQuantity.ToString("N2"));
                                            table.Cell().AlignRight().Text($"R$ {mat.UnitCost:N2}");
                                            table.Cell().AlignRight().Text($"R$ {subtotal:N2}");
                                            table.Cell().AlignCenter().Text(fornecidoCliente).FontColor(mat.IsClientProvided ? Colors.Red.Medium : Colors.Black);
                                        }
                                    });
                                }

                                // Tabela de Mão de Obra da Etapa
                                if (stage.Labors != null && stage.Labors.Any())
                                {
                                    col.Item().PaddingTop(10).Text("Mão de Obra").SemiBold();
                                    col.Item().PaddingTop(2).Table(table =>
                                    {
                                        table.ColumnsDefinition(columns =>
                                        {
                                            columns.RelativeColumn(2); // Função/Role
                                            columns.RelativeColumn(1); // Horas
                                            columns.RelativeColumn(1); // Valor Hora
                                            columns.RelativeColumn(1); // Encargos
                                            columns.RelativeColumn(1); // Subtotal
                                        });

                                        table.Header(header =>
                                        {
                                            header.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Text("Função (Role ID)").SemiBold();
                                            header.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).AlignRight().Text("Horas").SemiBold();
                                            header.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).AlignRight().Text("Valor/Hora").SemiBold();
                                            header.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).AlignRight().Text("Encargos").SemiBold();
                                            header.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).AlignRight().Text("Subtotal").SemiBold();
                                        });

                                        foreach (var lab in stage.Labors)
                                        {
                                            var subtotal = lab.PlannedHours * lab.HourlyRate; // Ajuste o cálculo se os encargos somarem aqui

                                            table.Cell().Text($"Função ID: {lab.RoleId}"); // Substitua pelo nome da função se tiver no DTO
                                            table.Cell().AlignRight().Text(lab.PlannedHours.ToString("N2"));
                                            table.Cell().AlignRight().Text($"R$ {lab.HourlyRate:N2}");
                                            table.Cell().AlignRight().Text($"R$ {lab.SocialCharges:N2}");
                                            table.Cell().AlignRight().Text($"R$ {subtotal:N2}");
                                        }
                                    });
                                }
                            }
                        }

                        // 3. OBSERVAÇÕES
                        if (!string.IsNullOrWhiteSpace(budget.Observation))
                        {
                            col.Item().PaddingTop(20).Text("Observações").FontSize(14).SemiBold();
                            col.Item().PaddingTop(5).Text(budget.Observation).Italic();
                        }

                        // 4. RESUMO FINANCEIRO E TOTAIS (Alinhado à direita no fim da página)
                        col.Item().PaddingTop(30).LineHorizontal(2).LineColor("#ff6600");
                        col.Item().PaddingTop(10).AlignRight().Column(c =>
                        {
                            c.Item().Text($"Total de Materiais: R$ {budget.TotalMaterialsValue:N2}").FontSize(12);
                            c.Item().Text($"Total de Etapas/Mão de Obra: R$ {budget.TotalStepsValue:N2}").FontSize(12);
                            c.Item().PaddingTop(5).Text($"VALOR TOTAL: R$ {budget.TotalValue:N2}")
                                .FontSize(18).ExtraBold().FontColor("#ff6600");
                        });
                    });

                    // RODAPÉ COM PAGINAÇÃO
                    page.Footer().AlignCenter().Text(x =>
                    {
                        x.Span("Página ");
                        x.CurrentPageNumber();
                        x.Span(" de ");
                        x.TotalPages();
                    });
                });
            }).GeneratePdf();
        }

        //public byte[] GerarPdfBytes(Domain.Entities.Budget budget)
        //{
        //    // QuestPDF precisa da licença comunitária para uso gratuito
        //    QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;

        //    return QuestPDF.Fluent.Document.Create(container =>
        //    {
        //        container.Page(page =>
        //        {
        //            page.Margin(50);
        //            page.Header().Text($"Orçamento: {budget.Description}").FontSize(20).FontColor("#ff6600");

        //            page.Content().Column(col => {
        //                col.Item().Text($"Cliente: {budget.Client?.FantasyName}");
        //                col.Item().Text($"Valor Total: R$ {budget.TotalValue:N2}");
        //                col.Item().PaddingTop(10).Text("Endereço da Obra:").Bold();
        //                col.Item().Text($"{budget.Street}, {budget.Number} - {budget.City}/{budget.StateAbbreviation}");
        //            });
        //        });
        //    }).GeneratePdf();
        //}
    }
}
