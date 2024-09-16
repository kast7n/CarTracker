using WebApp.Models;

namespace WebApp.ViewModels
{
    public class VehicleDriversViewModel
    {
        
        public VehicleDriver VehicleDriver { get; set; } = new VehicleDriver();
        public IEnumerable<Driver> Drivers { get; set; } = new List<Driver>();
        public IEnumerable<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    }
}
