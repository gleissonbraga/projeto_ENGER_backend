using ENGER.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace ENGER.Application.Validation
{
    public static class Validation
    {
        // Valida se o campo está preenchido
        public static void InputRequired(string value, string fieldName, List<ValidationError> errors)
        {
            if (string.IsNullOrEmpty(value))
                errors.Add(new ValidationError(fieldName, "O campo é obrigatório."));
        }

        // Valida formato de e-mail
        public static void EmailFormat(string email, string fieldName, List<ValidationError> errors)
        {
            if (string.IsNullOrEmpty(email)) return;

            var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            if (!regex.IsMatch(email))
                errors.Add(new ValidationError(fieldName, "O formato informado é inválido."));
        }

        // Valida se contém APENAS números (CNPJ, CEP)
        public static void OnlyNumbers(string value, string fieldName, List<ValidationError> errors)
        {
            if (string.IsNullOrEmpty(value)) return;

            if (!value.All(char.IsDigit))
                errors.Add(new ValidationError(fieldName, "O campo deve conter apenas números."));
        }

        // Valida se é um número decimal (Aceita ponto ou vírgula)
        public static void IsDecimal(string value, string fieldName, List<ValidationError> errors)
        {
            if (string.IsNullOrEmpty(value)) return;

            bool isValid = decimal.TryParse(value, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out _) ||
                           decimal.TryParse(value, System.Globalization.NumberStyles.Any, new System.Globalization.CultureInfo("pt-BR"), out _);

            if (!isValid)
                errors.Add(new ValidationError(fieldName, "Este campo deve ser um número decimal válido."));
        }

        // Valida tamanho máximo
        public static void MaxLength(string value, int max, string fieldName, List<ValidationError> errors)
        {
            if (!string.IsNullOrEmpty(value) && value.Length > max)
                errors.Add(new ValidationError(fieldName, $"O campo deve ter no máximo {max} caracteres."));
        }
    }
}