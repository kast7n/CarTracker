using WebApp.Models;

namespace WebApp.Repositories.Interfaces
{
    public interface IMaintenanceRepository
    {
        IEnumerable<Maintenance> GetMaintenanceHistory();
        Maintenance? GetMaintenanceById(int maintenanceId);
        IEnumerable<Maintenance> GetMaintenanceByVehicleId(int vehicleId);
        void Insert(Maintenance maintenance);
        void Update(int maintenanceId, Maintenance updatedMaintenance);
        void Delete(int maintenanceId);
    }
}
