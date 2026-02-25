using System.ComponentModel.DataAnnotations;

namespace mvc.starter.template.Web.Validation
{
    public class RequiredIfAttribute : ValidationAttribute
    {
        public string PropertyName { get; }
        public object ExpectedValue { get; }

        public RequiredIfAttribute(string propertyName, object expectedValue)
        {
            PropertyName = propertyName;
            ExpectedValue = expectedValue;
        }

        protected override ValidationResult IsValid(
            object value,
            ValidationContext validationContext)
        {
            var instance = validationContext.ObjectInstance;

            var property = instance.GetType()
                .GetProperty(PropertyName);

            if (property == null)
                return ValidationResult.Success;

            var dependentValue = property.GetValue(instance);

            if (!Equals(dependentValue, ExpectedValue))
                return ValidationResult.Success;

            if (string.IsNullOrWhiteSpace(value?.ToString()))
                return new ValidationResult(ErrorMessage);

            return ValidationResult.Success;
        }
    }
}
