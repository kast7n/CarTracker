using WebApp.Models;

namespace WebApp.ViewModels
{
    public class MaintenanceViewModel
    {
        public Maintenance Maintenance { get; set; } = new Maintenance();
        public IEnumerable<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    }
}
