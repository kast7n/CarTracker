using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Driver
    {
        public int DriverHistoryId { get; set; }
        [Required]
        [Display(Name = "Vehicle")]
        public int VehicleId { get; set; }
        [Required]
        [Display(Name = "Driver Name")]
        public string? DriverName { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }

        public Vehicle? Vehicle { get; set; }
    }
}
