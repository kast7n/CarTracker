using WebApp.Models;

namespace WebApp.Repositories.Interfaces
{
    public interface IVehicleTypeRepository
    {
        void Insert(VehicleType vehicleType);
        IEnumerable<VehicleType> GetVehicleTypes();
        VehicleType? GetVehicleTypeById(int typeId);
        void Update(int typeId, VehicleType updatedVehicleType);
        void Delete(int typeId);
    }
}
