using ENGER.Application.Exceptions;
using ENGER.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENGER.Application.UseCases.Card.GetByIdCompany
{
    public class GetCardUseCase
    {
        public readonly ICardRepository _repository;

        public GetCardUseCase(ICardRepository repository)
        {
            _repository = repository;
        }

        public async Task<Domain.Entities.Card> ExecuteAsync(int companyId) 
        {
            Domain.Entities.Card objCard = await _repository.GetCardByIdCompanyAsync(companyId);

            if (objCard == null) throw new ApplicException("card", "Cartão não encontrado");

            return objCard;
        }
    }
}
