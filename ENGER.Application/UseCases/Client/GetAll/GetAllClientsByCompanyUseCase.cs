using ENGER.Application.DTOs.Position;
using ENGER.Application.DTOs.Subsciption;
using ENGER.Application.Exceptions;
using ENGER.Domain.Entities;
using ENGER.Domain.Exceptions;
using ENGER.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ENGER.Application.UseCases.Client.GetAll
{
    public class GetAllClientsByCompanyUseCase
    {
        private readonly IClientRepository _repository;

        public GetAllClientsByCompanyUseCase(IClientRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Domain.Entities.Client>> ExecuteAsync(int companyId)
        {
            IEnumerable<Domain.Entities.Client> ieClients = await _repository.GetByCompanyIdAsync(companyId);

            return ieClients;
        }
    }
}
