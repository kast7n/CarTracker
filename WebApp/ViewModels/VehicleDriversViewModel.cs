using WebApp.Models;

namespace WebApp.ViewModels
{
    public class VehicleDriversViewModel
    {
        public VehicleDriver VehicleDriver { get; set; } = new VehicleDriver();
        public IEnumerable<Driver> drivers { get; set; } = new List<Driver>();
    }
}
