using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Maintenance
    {
        public int MaintenanceId { get; set; }
        [Required]
        [Display(Name = "Vehicle")]
        public int VehicleId { get; set; }
        [Required]
        [Display(Name = "Type")]
        public string? MaintenanceType { get; set; }
        [Required]
        [Display(Name = "Date")]
        public DateTime MaintenanceDate { get; set; }
        [Required]
        public string? Description { get; set; }
        public Vehicle? Vehicle { get; set; }

    }
}
