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

        public CreateSubscriptionUseCase(ISubscriptionRepository repository, ICardRepository cardRepository)
        {
            _repository = repository;
            _cardRepository = cardRepository;
        }

        public async Task ExecuteAsync(SubscriptionRequestDTO request)
        {
            DateTime expirationDate = DateTime.UtcNow.AddDays(30);

            Card objCard = new Card(request.CardRequestDTO.cardToken, request.CardRequestDTO.lastCardNumber, request.CardRequestDTO.brand, request.CardRequestDTO.expirationDateCard, request.companyId);

            await _cardRepository.AddAsync(objCard);

            Domain.Entities.Subscription objSubscription = new Domain.Entities.Subscription(Guid.NewGuid(), expirationDate, Status.SubPending, DateTime.UtcNow, 1);

            await _repository.AddAsync(objSubscription);
        }
    }
}
