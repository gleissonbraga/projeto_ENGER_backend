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

namespace ENGER.Application.UseCases.Client.GetById
{
    public class GetByIdClientUseCase
    {
        private readonly IClientRepository _repository;

        public GetByIdClientUseCase(IClientRepository repository)
        {
            _repository = repository;
        }

        public async Task<Domain.Entities.Client> ExecuteAsync(int intCompanyId, int intClientId)
        {
            Domain.Entities.Client ieClient = await _repository.GetByIdAsync(intClientId, intCompanyId);

            return ieClient;
        }
    }
}
