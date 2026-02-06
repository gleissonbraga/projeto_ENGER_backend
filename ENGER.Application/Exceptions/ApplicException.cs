using ENGER.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENGER.Application.Exceptions
{
    public class ApplicException : AppValidationException
    {
        public ApplicException(string field, string message) : base(field, message) { }
        public ApplicException(List<ValidationError> errors) : base(errors) { }
    }
}
