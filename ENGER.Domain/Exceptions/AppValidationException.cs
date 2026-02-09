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
        public List<ValidationError> lstErrors { get; protected set; } = new List<ValidationError>();

        // Construtor para um único erro (ex: Erro de regra de negócio)
        public AppValidationException(string field, string message) : base(message)
        {
            lstErrors.Add(new ValidationError(field, message));
        }

        // Construtor para lista de erros (ex: Validação de formulário)
        public AppValidationException(List<ValidationError> errors) : base("Ocorreram um ou mais erros de validação.")
        {
            lstErrors = errors;
        }
    }

    public record ValidationError(string Field, string Message);
}
