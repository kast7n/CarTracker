using WebApp.Models;

namespace WebApp.Repositories.Interfaces
{
    public interface IVehicleDriverRepository
    {
        void Insert(VehicleDriver vehicleDriver);
        IEnumerable<VehicleDriver> GetVehicleDrivers();
        VehicleDriver? GetVehicleDriver(int vehicleDriverId);
        IEnumerable<VehicleDriver> GetVehicleDriversByDriverId(int driverId);
        IEnumerable<VehicleDriver> GetVehicleDriversByVehicleId(int vehicleId, bool loadInfo = false);
        void Update(int vehicleDriverId, VehicleDriver updatedVehicleDriver);
        void Delete(int vehicleDriverId);
    }
}
