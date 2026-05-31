using ENGER.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENGER.Application.UseCases.Construction.GetAll
{
    public class GetAllConstructionsUseCase
    {
        private readonly IConstructionRepository _repository;
        private readonly IBudgetRepository _budgetRepository;

        public GetAllConstructionsUseCase(IConstructionRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Domain.Entities.Construction>> ExecuteAsync(int companyId)
        {
            IEnumerable<Domain.Entities.Construction> lstContruction = await _repository.GetByCompanyAsync(companyId);

            return lstContruction;
        }
    }
}
