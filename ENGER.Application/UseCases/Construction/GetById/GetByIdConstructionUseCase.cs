using ENGER.Application.Exceptions;
using ENGER.Domain.Interfaces.Repositories;

namespace ENGER.Application.UseCases.Construction.GetById
{
    public class GetByIdConstructionUseCase
    {
        private readonly IConstructionRepository _repository;

        public GetByIdConstructionUseCase(IConstructionRepository repository)
        {
            _repository = repository;
        }

        public async Task<Domain.Entities.Construction> ExecuteAsync(int constructionId, int companyId)
        {
            Domain.Entities.Construction objContruction = await _repository.GetByIdAsync(constructionId, companyId);

            if (objContruction == null)
                throw new ApplicException("Construction", "Obra não encontrada");

            return objContruction;
        }
    }
}
