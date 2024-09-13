using WebApp.Data;
using WebApp.Models;
using WebApp.Repositories.Interfaces;

namespace WebApp.Repositories
{
    public class DriversRepository : IDriverRepository
    {
        private readonly VehicleInfoManagerContext db;
        public DriversRepository(VehicleInfoManagerContext db)
        {
            this.db = db;
        }

        public void Delete(int driverId)
        {
            var driver = db.Drivers.Find(driverId);
            if (driver == null) return;
            db.Drivers.Remove(driver);
            db.SaveChanges();
        }

        public Driver? GetDriverById(int driverId)
        {
            return db.Drivers.FirstOrDefault(x => x.DriverId == driverId);
        }

        public IEnumerable<Driver> GetDrivers()
        {
            return db.Drivers.OrderBy(x => x.DriverId).ToList();
        }

        public void Insert(Driver driver)
        {
            db.Drivers.Add(driver);
            db.SaveChanges();
        }

        public void Update(int driverId, Driver updatedDriver)
        {
            if (driverId != updatedDriver.DriverId) return;
            var driver = db.Drivers.Find(driverId);
            if (driver == null) return;
            driver.Name = updatedDriver.Name;
            driver.LicenseNumber = updatedDriver.LicenseNumber;
            db.SaveChanges();
        }
    }
}
