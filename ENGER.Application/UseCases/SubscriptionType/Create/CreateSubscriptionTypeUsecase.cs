using ENGER.Application.DTOs.Company;
using ENGER.Application.DTOs.SubsciptionType;
using ENGER.Application.Exceptions;
using ENGER.Domain.Enums;
using ENGER.Domain.Exceptions;
using ENGER.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ENGER.Application.UseCases.SubscriptionType.Create
{
    public class CreateSubscriptionTypeUsecase
    {
        private readonly ISubscriptionTypeRepository _repository;

        public CreateSubscriptionTypeUsecase(ISubscriptionTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<SubscriptionTypeRequestDTO> ExecuteAsync(SubscriptionTypeRequestDTO request)
        {
            var errors = new List<ValidationError>();

            Validation.Validation.InputRequired(request.descriptionSubscriptionType, "descriptionSubscriptionType", errors);
            Validation.Validation.MaxLength(request.descriptionSubscriptionType, 40, "descriptionSubscriptionType", errors);

            Validation.Validation.OnlyNumbers(request.subscriptionValue.ToString(), "subscriptionValue", errors);
            Validation.Validation.IsDecimal(request.subscriptionValue.ToString(), "subscriptionValue", errors);

            if (errors.Count > 0) throw new ApplicException(errors);

            Domain.Entities.SubscriptionType objSubscriptionType = new Domain.Entities.SubscriptionType(request.descriptionSubscriptionType, request.subscriptionValue);

            await _repository.AddAsync(objSubscriptionType);

            return request;
        }
    }
}
