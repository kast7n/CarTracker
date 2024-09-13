using WebApp.Models;

namespace WebApp.Repositories.Interfaces
{
    public interface IDriverRepository
    {
        IEnumerable<Driver> GetDrivers();
        Driver? GetDriverById(int driverId);
        void Insert(Driver driver);
        void Update(int driverId, Driver updatedDriver);
        void Delete(int driverId);
    }
}
