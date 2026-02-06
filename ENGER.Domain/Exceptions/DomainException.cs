using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENGER.Domain.Exceptions
{
    public class DomainException : AppValidationException
    {
        public DomainException(string field, string message) : base(field, message) { }
        public DomainException(List<ValidationError> errors) : base(errors) { }
    }
}
