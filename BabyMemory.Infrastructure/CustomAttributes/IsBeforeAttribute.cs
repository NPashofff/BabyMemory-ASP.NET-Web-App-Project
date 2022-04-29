#nullable disable
namespace BabyMemory.Core.CustomAttributes
{
    using System.ComponentModel.DataAnnotations;

    public class IsBeforeAttribute : ValidationAttribute
    {
        private readonly string _propertyToCompare;

        public IsBeforeAttribute(string propertyToCompare, string errorMessage = "")
        {
            _propertyToCompare = propertyToCompare;
            ErrorMessage = errorMessage;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                DateTime dateToCompare = (DateTime)validationContext
                    .ObjectType
                    .GetProperty(_propertyToCompare)!
                    .GetValue(validationContext.ObjectInstance)!;

                if ((DateTime)value < dateToCompare)
                {
                    return ValidationResult.Success;
                }
            }
            catch (Exception)
            { }

            return new ValidationResult(ErrorMessage);
        }
    }
}
