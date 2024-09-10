using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Vehicle
    {
        public int VehicleId { get; set; }
        [Required]
        [Display(Name = "Model")]
        public string? VehicleModel { get; set; }
        [Required]
        [Display(Name = "Plate Number")]
        public string? PlateNumber { get; set; }
        [Required]
        [Display(Name = "Seats")]
        [Range(1, int.MaxValue)]
        public int NumberOfSeats { get; set; }
        [Required]
        public string? Color { get; set; }
        [Required]
        public string? Manufacturer { get; set; }


    }
}
