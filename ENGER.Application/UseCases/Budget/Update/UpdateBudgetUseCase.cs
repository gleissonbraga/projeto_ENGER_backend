using ENGER.Application.DTOs.Budget;
using ENGER.Application.Exceptions;
using ENGER.Domain.Enums;
using ENGER.Domain.Exceptions;
using ENGER.Domain.Interfaces.Repositories;

namespace ENGER.Application.UseCases.Budget.Update
{
    public class UpdateBudgetUseCase
    {
        public readonly IBudgetRepository _repository;
        public readonly ICompanyRepository _companyRepository;
        public readonly IClientRepository _clientRepository;

        public UpdateBudgetUseCase(IBudgetRepository repository, ICompanyRepository companyRepository, IClientRepository clientRepository)
        {
            _repository = repository;
            _companyRepository = companyRepository;
            _clientRepository = clientRepository;
        }

        public async Task<BudgetResponseDTO> ExecuteAsync(BudgetRequestDTO request, int budgetId, int companyId)
        {
            var errors = new List<ValidationError>();

            // --- 1. VALIDAÇÕES BÁSICAS ---
            Validation.Validation.InputRequired(request.description, "description", errors);
            Validation.Validation.MaxLength(request.description, 255, "description", errors);

            // Validações de Endereço (Novos campos)
            Validation.Validation.InputRequired(request.street, "street", errors);
            Validation.Validation.InputRequired(request.city, "city", errors);
            Validation.Validation.InputRequired(request.stateAbbreviation, "stateAbbreviation", errors);

            if (request.Stages == null || !request.Stages.Any())
                errors.Add(new ValidationError("stages", "At least one stage is required"));

            // --- 2. BUSCA O ORÇAMENTO EXISTENTE ---
            // É vital carregar com Include para poder manipular as coleções
            var objBudget = await _repository.GetByIdAsync(budgetId, companyId);
            if (objBudget == null)
            {
                errors.Add(new ValidationError("budget", "Orçamento não encontrado"));
                throw new ApplicException(errors);
            }

            // Impede edição se já estiver aprovado ou em obra
            if (objBudget.Status == Status.BudApproved)
            {
                errors.Add(new ValidationError("budget", "Orçamentos aprovados não podem ser editados."));
                throw new ApplicException(errors);
            }

            // --- 3. VALIDAÇÃO DE CLIENTE E EMPRESA ---
            var objCClient = await _clientRepository.GetByIdAsync(companyId, (int)request.clientId);
            if (objCClient == null)
                errors.Add(new ValidationError("client", "Cliente não encontrado"));

            if (errors.Any()) throw new ApplicException(errors);

            objBudget.ClientId = request.clientId;
            objBudget.UserId = request.userId;
            objBudget.Description = request.description;
            objBudget.TotalStepsValue = request.totalStepsValue;
            objBudget.TotalMaterialsValue = request.totalMaterialsValue;
            objBudget.TotalValue = request.totalValue;
            objBudget.Observation = request.observation;
            objBudget.Street = request.street;
            objBudget.Number = request.number;
            objBudget.City = request.city;
            objBudget.Neighborhood = request.neighborhood;
            objBudget.ZipCode = request.zipCode;
            objBudget.StateAbbreviation = request.stateAbbreviation;
            objBudget.StateDescription = request.stateDescription;

            objBudget.Stages.Clear();

            foreach (var stageDto in request.Stages)
            {
                var stage = new Domain.Entities.BudgetStage(
                    stageDto.description,
                    stageDto.order,
                    Status.SubInProcess
                );

                if (stageDto.Materials != null)
                {
                    foreach (var m in stageDto.Materials)
                        stage.Materials.Add(new Domain.Entities.BudgetMaterial(m.Description, m.Unit, m.PlannedQuantity, m.UnitCost, m.IsClientProvided));
                }

                if (stageDto.Labors != null)
                {
                    foreach (var l in stageDto.Labors)
                        stage.Labors.Add(new Domain.Entities.BudgetLabor(l.RoleId, l.PlannedHours, l.HourlyRate, l.SocialCharges));
                }

                objBudget.Stages.Add(stage);
            }

            // --- 6. PERSISTÊNCIA ---
            await _repository.UpdateAsync(objBudget);

            // Retorno do DTO (mesma lógica do Create)
            return MapToResponse(objBudget);
        }

        private BudgetResponseDTO MapToResponse(Domain.Entities.Budget obj)
        {
            // Aqui vai o mapeamento para o BudgetResponseDTO que você já tem no Create
            // ... (Omitido para brevidade, mas segue o mesmo padrão do seu Create)
            return null;
        }
    }
}