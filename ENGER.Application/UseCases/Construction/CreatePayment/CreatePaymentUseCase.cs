using ENGER.Application.DTOs.Construction;
using ENGER.Domain.Entities;
using ENGER.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENGER.Application.UseCases.Construction.CreatePayment
{
    public class CreatePaymentUseCase
    {
        private readonly IConstructionRepository _repository;
        private readonly IBudgetRepository _budgetRepository;

        public CreatePaymentUseCase(IConstructionRepository repository, IBudgetRepository repositoryBudget)
        {
            _repository = repository;
            _budgetRepository = repositoryBudget;
        }

        public async Task<Domain.Entities.ConstructionPayment> ExecuteAsync(int constructionId,int companyId, ConstructionPaymentDTO payment)
        {
            ENGER.Domain.Entities.Construction objConstruction = await _repository.GetByIdAsync(constructionId, companyId);

            ConstructionPayment objPayment = new ConstructionPayment(DateTime.UtcNow, 1, constructionId, payment.StageId, payment.PaymentValue);

            objConstruction.TotalPaidValue = objConstruction.TotalPaidValue + payment.PaymentValue;

            await _repository.UpdateAsync(objConstruction);

            ConstructionPayment response = await _repository.AddPaymentAsync(objPayment);

            return response;
        }
    }
}
