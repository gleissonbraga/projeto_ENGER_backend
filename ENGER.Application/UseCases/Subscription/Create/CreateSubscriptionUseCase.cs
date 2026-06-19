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

        public CreateSubscriptionUseCase(ISubscriptionRepository repository, ICardRepository cardRepository, IPaymentServiceRepository paymentServiceRepository, ISubscriptionTypeRepository subscriptionTypeRepository,
            ICompanyRepository companyRepository)
        {
            _repository = repository;
            _cardRepository = cardRepository;
            _paymentServiceRepository = paymentServiceRepository;
            _subscriptionTypeRepository = subscriptionTypeRepository;
            _companyRepository = companyRepository;
        }

        public async Task<int> ExecuteAsync(int companyId, SubscriptionRequestDTO request)
        {
            DateTime expirationDate = DateTime.UtcNow.AddDays(30);
            DateTime nextBilling = DateTime.UtcNow.AddMonths(1);

            Domain.Entities.Card objCard = null;
            Domain.Entities.Subscription objSubscription = null;
            Domain.Entities.Subscription objSubscriptionReturned = null;

            int subscriptionId = 0;
            (int intStatus, string subscriptionIdMP) = (0, "");

            var errors = new List<ValidationError>();

            Domain.Entities.Company objCompany = await _companyRepository.GetByIdAsync(companyId);

            if (objCompany == null) throw new ApplicException("company", "Empresa não encontrada");

            //Domain.Entities.Card objCardExist = await _cardRepository.GetCardByIdCompanyAsync(companyId);

            //if (objCardExist == null)
            //{
            //    objCard = await _paymentServiceRepository.AddCardCustomerAsync(request.CardRequestDTO.cardToken, objCompany.Email, objCompany.CompanyId);
            //    await _cardRepository.AddAsync(objCard);
            //}
            //else
            //{
            //    objCard = objCardExist;
            //}

            Domain.Entities.SubscriptionType objSubType = await _subscriptionTypeRepository.GetByIdAsync(request.subscriptionTypeId);

            if (objSubType == null) throw new ApplicException("subscritionType", "Assinatura não encontrada");

            if (objCompany.SubscriptionCode == null) 
            {
                objSubscription = new Domain.Entities.Subscription(Guid.NewGuid(), DateTime.UtcNow, Status.SubPending, DateTime.UtcNow,
                                                                                                                    request.subscriptionTypeId, nextBilling, expirationDate, null);
                subscriptionId = await _repository.AddAsync(objSubscription);

                (intStatus, subscriptionIdMP) = await _paymentServiceRepository.CreatePaymentAsync(objCard, objSubType.SubscriptionValue, objSubscription.SubscriptionCode
                                                                                , request.CardRequestDTO.cardToken, objCompany, objSubscription, objSubType);

                objCompany.SubscriptionCode = objSubscription.SubscriptionCode;
                await _companyRepository.UpdateAsync(objCompany);

                objSubscriptionReturned = await _repository.GetByIdAsync(subscriptionId);
            }
            else
            {
                objSubscription = await _repository.GetBySubscriptionKeyAccess((Guid)objCompany.SubscriptionCode);

                if(objSubscription == null) throw new ApplicException("error", "Empresa sem chave de acesso");

                (intStatus, subscriptionIdMP) = await _paymentServiceRepository.CreatePaymentAsync(objCard, objSubType.SubscriptionValue, objSubscription.SubscriptionCode
                                                                               , request.CardRequestDTO.cardToken, objCompany, objSubscription, objSubType);

                objSubscriptionReturned = await _repository.GetByIdAsync(objSubscription.SubscriptionId);
            }

            if (objSubscriptionReturned == null) throw new ApplicException("error", "Problemas na inserção dos dados do Mercado Pago");

            objSubscriptionReturned.SubscriptionIdMercadoPago = subscriptionIdMP;
            objSubscriptionReturned.StatusSubscription = (Status)intStatus;

            if (intStatus == (int)Status.SubRejected || intStatus == (int)Status.SubPending || intStatus == (int)Status.SubInProcess || intStatus == (int)Status.SubCancelled) 
            {
                objSubscriptionReturned.ExpirationDate = DateTime.UtcNow;
                objSubscriptionReturned.StartDate = DateTime.UtcNow;
                objSubscriptionReturned.PaymentDate = DateTime.UtcNow;
            }

            await _repository.UpdateAsync(objSubscriptionReturned);

            return intStatus;
        }
    }
}
