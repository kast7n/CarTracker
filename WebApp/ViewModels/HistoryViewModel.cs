using WebApp.Models;

namespace WebApp.ViewModels
{
    public class HistoryViewModel
    {
        public int SelectedVehicleId { get; set; }
        public IEnumerable<VehicleType> VehicleTypes { get; set; } = new List<VehicleType>();
        public IEnumerable<Manufacturer> Manufacturers { get; set; } = new List<Manufacturer>();
        public IEnumerable<Vehicle> Vehicles { get; set; } = new List<Vehicle>();

        public int? SelectedTypeId { get; set; }
        public int? SelectedManufacturerId { get; set; }

    }
}
