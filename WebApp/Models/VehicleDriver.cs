using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class VehicleDriver
    {
        public int VehicleDriverId { get; set; }

        [Required]
        [Display(Name = "Driver")]
        public int DriverId { get; set; }
        public Driver? Driver {  get; set; }   
 
        [Required]
        [Display(Name = "Vehicle")]
        public int VehicleId { get; set; }
        public Vehicle? Vehicle { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

    }
}
