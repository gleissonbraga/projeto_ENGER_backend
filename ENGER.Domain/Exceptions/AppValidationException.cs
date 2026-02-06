using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ENGER.Domain.Exceptions
{
    public class AppValidationException : Exception
    {
        public List<ValidationError> lstErrors { get; } = new List<ValidationError>();

        public AppValidationException(string Field, string message) : base(message) { }

        public AppValidationException(List<ValidationError> errors) : base("Ocorreram um ou mais erros de validação.")
        {
            lstErrors = errors;
        }
    }

    public record ValidationError(string Field, string Message);
}
