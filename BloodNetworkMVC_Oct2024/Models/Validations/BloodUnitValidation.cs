using System.ComponentModel.DataAnnotations;

namespace BloodNetworkMVC_Oct2024.Models.Validations
{
    public class BloodUnitValidation
    {
        public static ValidationResult ValidateExpirationDate(DateTime expirationDate, ValidationContext context)
        {
            var bloodUnit = (BloodUnit)context.ObjectInstance;

            // Blood can't expire before collection
            if (expirationDate <= bloodUnit.CollectionDate)
            {
                return new ValidationResult("Expiration date must be after collection date");
            }

            // Standard blood shelf life is 42 days
            var maxExpirationDate = bloodUnit.CollectionDate.AddDays(42);
            if (expirationDate > maxExpirationDate)
            {
                return new ValidationResult("Blood units cannot be stored for more than 42 days");
            }

            return ValidationResult.Success;
        }
    }
}
