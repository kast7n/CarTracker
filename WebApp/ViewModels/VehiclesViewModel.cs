using WebApp.Models;

namespace WebApp.ViewModels
{
    public class VehiclesViewModel
    {
        public Vehicle Vehicle { get; set; } = new Vehicle();
        public IEnumerable<VehicleType> VehicleTypes { get; set; } = new List<VehicleType>();
        public IEnumerable<Manufacturer> Manufacturers { get; set; } = new List<Manufacturer>();
    }
}
