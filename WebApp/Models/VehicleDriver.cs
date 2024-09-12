using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class VehicleDriver
    {
        public int AssignmentId { get; set; }

        public int DriverId { get; set; }
        public Driver? Driver {  get; set; }   

        public string? DriverName { get; set; }
        
        [Required]
        [Display(Name = "Vehicle")]
        public int VehicleId { get; set; }
        public Vehicle? Vehicle { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }

    }
}
