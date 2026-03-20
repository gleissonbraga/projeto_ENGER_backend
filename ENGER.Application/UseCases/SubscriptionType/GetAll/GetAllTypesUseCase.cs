using ENGER.Application.DTOs.Company;
using ENGER.Application.DTOs.User;
using ENGER.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENGER.Application.UseCases.User.GetAll
{
    public class GetAllTypesUseCase
    {
        public readonly ISubscriptionTypeRepository _repository;

        public GetAllTypesUseCase(ISubscriptionTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ENGER.Domain.Entities.SubscriptionType>> ExecuteAsync()
        {
            var subs = await _repository.GetAllAsync();

            return subs;
        }
    }
}
