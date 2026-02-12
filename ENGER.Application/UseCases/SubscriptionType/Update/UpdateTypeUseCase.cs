using ENGER.Application.DTOs.Company;
using ENGER.Application.DTOs.SubsciptionType;
using ENGER.Application.DTOs.User;
using ENGER.Application.Exceptions;
using ENGER.Domain.Exceptions;
using ENGER.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENGER.Application.UseCases.User.Update
{
    public class UpdateTypeUseCase
    {
        public readonly ISubscriptionTypeRepository _repository;

        public UpdateTypeUseCase(ISubscriptionTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<ENGER.Domain.Entities.SubscriptionType> ExecuteAsync(SubscriptionTypeRequestDTO request, int subTypeId)
        {
            var errors = new List<ValidationError>();

            Validation.Validation.InputRequired(request.descriptionSubscriptionType, "descriptionSubscriptionType", errors);
            Validation.Validation.MaxLength(request.descriptionSubscriptionType, 40, "descriptionSubscriptionType", errors);

            Validation.Validation.IsDecimal(request.subscriptionValue.ToString(), "descriptionSubscriptionType", errors);

            if (errors.Count > 0) throw new ApplicException(errors);

            Domain.Entities.SubscriptionType sub = await _repository.GetByIdAsync(subTypeId);

            if (sub == null) throw new ApplicException("subscriptonType", "Tipo de assinatura não encontrada");

            sub.DescriptionSubscriptionType = request.descriptionSubscriptionType;
            sub.SubscriptionValue = request.subscriptionValue;

            Domain.Entities.SubscriptionType subResponse = await _repository.UpdateAsync(sub);

            return subResponse;
        }
    }
}
