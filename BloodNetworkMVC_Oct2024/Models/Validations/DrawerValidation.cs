using System.ComponentModel.DataAnnotations;

namespace BloodNetworkMVC_Oct2024.Models.Validations
{
    public class DrawerValidation
    {
        public static ValidationResult ValidateCurrentCount(int currentCount, ValidationContext context)
        {
            var drawer = (Drawer)context.ObjectInstance;

            if (currentCount > drawer.Capacity)
            {
                return new ValidationResult($"Current count cannot exceed drawer capacity of {drawer.Capacity}");
            }

            return ValidationResult.Success;
        }
    }
}
