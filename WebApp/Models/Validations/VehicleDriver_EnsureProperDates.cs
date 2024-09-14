using System.ComponentModel.DataAnnotations;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Models.Validations
{
    public class VehicleDriver_EnsureProperDates : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var vehicleDriver = validationContext.ObjectInstance as VehicleDriver;

            if (vehicleDriver != null)
            {
                if (vehicleDriver.EndDate < vehicleDriver.StartDate)
                {
                    return new ValidationResult("End Date can't be smaller than Start Date");
                }

            }
            return ValidationResult.Success;
        }
    }
}
