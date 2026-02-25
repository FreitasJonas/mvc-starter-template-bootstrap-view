using System.ComponentModel.DataAnnotations;

namespace mvc.starter.template.Web.Validation
{
    public class PasswordConfirmationAttribute : ValidationAttribute
    {
        private readonly string _passwordProperty;

        public PasswordConfirmationAttribute(string passwordProperty)
        {
            _passwordProperty = passwordProperty;
        }

        protected override ValidationResult IsValid(
            object value,
            ValidationContext validationContext)
        {
            var passwordProp = validationContext.ObjectType
                .GetProperty(_passwordProperty);

            if (passwordProp == null)
                return ValidationResult.Success;

            var passwordValue = passwordProp
                .GetValue(validationContext.ObjectInstance)?
                .ToString();

            var confirmationValue = value?.ToString();

            // Se senha não foi informada → não valida nada
            if (string.IsNullOrWhiteSpace(passwordValue))
                return ValidationResult.Success;

            // Senha informada mas confirmação vazia → erro
            if (string.IsNullOrWhiteSpace(confirmationValue))
                return new ValidationResult(
                    ErrorMessage ?? "Password confirmation is required.");

            // Ambos informados mas diferentes → erro
            if (passwordValue != confirmationValue)
                return new ValidationResult(
                    ErrorMessage ?? "The passwords do not match.");

            return ValidationResult.Success;
        }
    }
}
