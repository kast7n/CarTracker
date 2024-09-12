using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Driver
    {
        public int DriverId { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        [Display(Name = "License Number")]
        public string? LicenseNumber { get; set; }

        //public List<VehicleDriver> VehicleDrivers { get; set; } = new List<VehicleDriver>();
    }
}
