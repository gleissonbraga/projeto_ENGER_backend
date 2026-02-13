using ENGER.Application.DTOs.Subsciption;
using ENGER.Application.DTOs.SubsciptionType;
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

namespace ENGER.Application.UseCases.Subscription.Create
{
    public class CreateSubscriptionUseCase
    {
        private readonly ISubscriptionRepository _repository;
        private readonly ICardRepository _cardRepository;
        private readonly IPaymentServiceRepository _paymentServiceRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly ISubscriptionTypeRepository _subscriptionTypeRepository;

        public CreateSubscriptionUseCase(ISubscriptionRepository repository, ICardRepository cardRepository, IPaymentServiceRepository paymentServiceRepository, ISubscriptionTypeRepository subscriptionTypeRepository)
        {
            _repository = repository;
            _cardRepository = cardRepository;
            _paymentServiceRepository = paymentServiceRepository;
            _subscriptionTypeRepository = subscriptionTypeRepository;
        }

        public async Task<int> ExecuteAsync(SubscriptionRequestDTO request)
        {
            DateTime expirationDate = DateTime.UtcNow.AddDays(30);
            DateTime nextBilling = DateTime.UtcNow.AddMonths(1);

            var errors = new List<ValidationError>();

            //Validation.Validation.InputRequired(request.descriptionSubscriptionType, "descriptionSubscriptionType", errors);
            //Validation.Validation.MaxLength(request.descriptionSubscriptionType, 40, "descriptionSubscriptionType", errors);

            Domain.Entities.Company objCompany = await _companyRepository.GetByIdAsync(request.companyId);

            if (objCompany == null) throw new ApplicException("company", "Empresa não encontrada");

            Domain.Entities.Card objCard = await _paymentServiceRepository.AddCardCustomerAsync(request.CardRequestDTO.cardToken, objCompany.Email, objCompany.CompanyId);
            await _cardRepository.AddAsync(objCard);

            Domain.Entities.Subscription objSubscription = new Domain.Entities.Subscription(Guid.NewGuid(), DateTime.UtcNow, Status.SubPending, DateTime.UtcNow, request.subscriptionTypeId, nextBilling, expirationDate);
            await _repository.AddAsync(objSubscription);

            Domain.Entities.SubscriptionType objSubType = await _subscriptionTypeRepository.GetByIdAsync(request.subscriptionTypeId);

            if (objSubType == null) throw new ApplicException("subscritionType", "Assinatura não encontrada");

            int intStatus = await _paymentServiceRepository.CreatePaymentAsync(objCard, objSubType.SubscriptionValue, objSubscription.SubscriptionCode);

            return intStatus;
        }
    }
}
