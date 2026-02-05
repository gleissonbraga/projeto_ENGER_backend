using ENGER.Application.DTOs.Company;
using ENGER.Application.DTOs.SubsciptionType;
using ENGER.Domain.Enums;
using ENGER.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENGER.Application.UseCases.SubscriptionType.Create
{
    public class CreateSubscriptionTypeUsecase
    {
        private readonly ISubscriptionTypeRepository _repository;

        public CreateSubscriptionTypeUsecase(ISubscriptionTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<CreateSubscriptionTypeRequest> ExecuteAsync(CreateSubscriptionTypeRequest request)
        {

            Domain.Entities.SubscriptionType objSubscriptionType = new Domain.Entities.SubscriptionType(request.descriptionSubscriptionType, request.subscriptionValue);

            await _repository.AddAsync(objSubscriptionType);
            return request;
        }
    }
}
