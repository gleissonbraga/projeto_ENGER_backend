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

namespace ENGER.Application.UseCases.Position.Create
{
    public class GetAllPositionByCompanyUseCase
    {
        private readonly IPositionRepository _repository;

        public GetAllPositionByCompanyUseCase(IPositionRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Domain.Entities.Position>> ExecuteAsync(int companyId)
        {
            IEnumerable<Domain.Entities.Position> objPositions = await _repository.GetAllPositions(companyId);

            return objPositions;
        }
    }
}
