using WebApp.Models;

namespace WebApp.ViewModels
{
    public class HistoryViewModel
    {
        public int SelectedVehicleId { get; set; }
        public IEnumerable<Vehicle> Vehicles { get; set; } = new List<Vehicle>();

    }
}
