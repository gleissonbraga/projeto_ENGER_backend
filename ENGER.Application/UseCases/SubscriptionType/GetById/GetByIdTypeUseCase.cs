using ENGER.Application.DTOs.Company;
using ENGER.Application.DTOs.User;
using ENGER.Application.Exceptions;
using ENGER.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENGER.Application.UseCases.User.GetById
{
    public class GetByIdTypeUseCase
    {
        public readonly ISubscriptionTypeRepository _repository;

        public GetByIdTypeUseCase(ISubscriptionTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<ENGER.Domain.Entities.SubscriptionType> ExecuteAsync(int subTypeId)
        {
            var sub = await _repository.GetByIdAsync(subTypeId);

            if (sub == null) throw new ApplicException("subscriptionType", "Tipo de Assinatura não encontrada");

            return sub;
        }
    }
}
