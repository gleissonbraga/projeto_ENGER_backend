using ENGER.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENGER.Application.UseCases.Construction.GetById
{
    internal class GetByIdConstructionUseCase
    {
        private readonly IConstructionRepository _repository;

        public GetByIdConstructionUseCase(IConstructionRepository repository)
        {
            _repository = repository;
        }

        public async Task<Domain.Entities.Construction> ExecuteAsync(int constructionId, int companyId)
        {
            Domain.Entities.Construction objContruction = await _repository.GetByIdAsync(constructionId, companyId);

            return objContruction;
        }
    }
}
