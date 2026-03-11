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
    public class UpdatePositionUseCase
    {
        private readonly IPositionRepository _repository;

        public UpdatePositionUseCase(IPositionRepository repository)
        {
            _repository = repository;
        }

        public async Task<Domain.Entities.Position> ExecuteAsync(int companyId, int positionId, PositionRequestDTO request)
        {
            var errors = new List<ValidationError>();

            Validation.Validation.InputRequired(request.DescriptionPosition, "descriptionPosition", errors);
            Validation.Validation.MaxLength(request.DescriptionPosition, 50, "username", errors);

            Domain.Entities.Position objPosition = await _repository.GetByIdAsync(positionId, companyId);

            if (objPosition == null) throw new ApplicException("position", "Cargo não encontrado");

            if (errors.Count > 0)
                throw new ApplicException(errors);

            objPosition.DescriptionPosition = request.DescriptionPosition;

            await _repository.UpdateAsync(objPosition);

            return objPosition;
        }
    }
}
