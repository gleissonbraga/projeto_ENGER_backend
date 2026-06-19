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
    public class CreatePositionUseCase
    {
        private readonly IPositionRepository _repository;

        public CreatePositionUseCase(IPositionRepository repository)
        {
            _repository = repository;
        }

        public async Task<Domain.Entities.Position> ExecuteAsync(int companyId, PositionRequestDTO request)
        {
            var errors = new List<ValidationError>();

            Validation.Validation.InputRequired(request.DescriptionPosition, "descriptionPosition", errors);
            Validation.Validation.MaxLength(request.DescriptionPosition, 50, "username", errors);

            if (errors.Count > 0)
                throw new ApplicException(errors);

            Domain.Entities.Position objPosition = new Domain.Entities.Position(request.DescriptionPosition, companyId);

            await _repository.UpdateAsync(objPosition);

            return objPosition;
        }
    }
}
