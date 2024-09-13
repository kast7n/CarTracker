using WebApp.Models;

namespace WebApp.Repositories.Interfaces
{
    public interface IVehicleRepository
    {
        IEnumerable<Vehicle> GetVehicles(bool loadInfo = false);
        IEnumerable<Vehicle> GetVehiclesByManufacturerId(int manufacturerId);
        IEnumerable<Vehicle> GetVehiclesByDriverId(int driverId);
        IEnumerable<Vehicle> GetvehicleByTypeId(int typeId);
        Vehicle? GetVehicleById(int vehicleId, bool loadInfo = false);
        void Insert(Vehicle vehicle);
        void Update(int vehicleId, Vehicle updatedVehicle);
        void Delete(int vehicleId);


    }
}
