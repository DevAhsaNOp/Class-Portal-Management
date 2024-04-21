using System.ComponentModel.DataAnnotations;

namespace Application.Common.Behaviours
{
    public class NotEqualAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;

        public NotEqualAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var currentValue = value as string;
            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);

            if (property == null)
                throw new ArgumentException("Property with this name not found");

            var comparisonValue = property.GetValue(validationContext.ObjectInstance) as string;

            if (currentValue != null && currentValue.Equals(comparisonValue))
            {
                return new ValidationResult(ErrorMessage ?? "The values cannot be the same.");
            }

            return ValidationResult.Success;
        }
    }
}
