using System.ComponentModel.DataAnnotations;

namespace Shop
{
    public class DefaultValueIfEmptyAttribute : ValidationAttribute
    {
        private readonly object _defaultValue;

        public DefaultValueIfEmptyAttribute(object defaultValue)
        {
            _defaultValue = defaultValue;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                var property = validationContext.ObjectType.GetProperty(validationContext.MemberName);
                if (property != null)
                {
                    property.SetValue(validationContext.ObjectInstance, _defaultValue);
                }
            }

            return ValidationResult.Success;
        }
    }
}
