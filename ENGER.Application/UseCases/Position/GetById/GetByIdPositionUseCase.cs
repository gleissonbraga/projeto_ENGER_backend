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
    public class GetByIdPositionUseCase
    {
        private readonly IPositionRepository _repository;

        public GetByIdPositionUseCase(IPositionRepository repository)
        {
            _repository = repository;
        }

        public async Task<Domain.Entities.Position> ExecuteAsync(int companyId, int positionId)
        {
            Domain.Entities.Position objPosition = await _repository.GetByIdAsync(positionId, companyId);

            return objPosition;
        }
    }
}
