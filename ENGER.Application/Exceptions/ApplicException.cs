using ENGER.Domain.Exceptions;

namespace ENGER.Application.Exceptions
{
    public class ApplicException : AppValidationException
    {
        public ApplicException(string field, string message) : base(field, message) { }
        public ApplicException(List<ValidationError> errors) : base(errors) { }
    }
}