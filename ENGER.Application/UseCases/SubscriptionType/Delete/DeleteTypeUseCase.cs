using ENGER.Application.DTOs.Company;
using ENGER.Application.DTOs.User;
using ENGER.Application.Exceptions;
using ENGER.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENGER.Application.UseCases.User.Delete
{
    public class DeleteTypeUseCase
    {
        public readonly ISubscriptionTypeRepository _repository;

        public DeleteTypeUseCase(ISubscriptionTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task ExecuteAsync(int subTypeid)
        {

            var sub = await _repository.GetByIdAsync(subTypeid);

            if (sub == null) throw new ApplicException("subscriptionType", "Tipo de Assinatura não encontrada");

            await _repository.DeleteAsync(sub);
        }
    }
}
