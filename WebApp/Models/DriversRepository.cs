using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApp.Models
{
    public static class DriversRepository
    {
        private static List<Driver> _drivers = new List<Driver>();

        static DriversRepository()
        {
            _drivers.AddRange(new[]
            {
                new Driver
                {
                    DriverId = 1,
                    Name = "Alice Johnson",
                    LicenseNumber = "D1234567",

                },
                new Driver
                {
                    DriverId = 2,
                    Name = "Bob Smith",
                    LicenseNumber = "D7654321",

                }
            });
        }

        public static List<Driver> GetDrivers() => _drivers;

        public static Driver? GetDriverById(int driverId)
        {
            var driver = _drivers.FirstOrDefault(x => x.DriverId == driverId);
            if (driver != null)
            {
                driver.VehicleDrivers = VehicleDriverRepository.GetVehicleDrivers().Where(vd => vd.DriverId == driverId).ToList();
                return driver;
            }
            return null;
        }

        public static void AddDriver(Driver driver)
        {
            if (driver.DriverId == 0)
            {
                driver.DriverId = _drivers.Any() ? _drivers.Max(x => x.DriverId) + 1 : 1;
            }

            // Retrieve and set vehicle drivers for the new driver
            driver.VehicleDrivers = VehicleDriverRepository.GetVehicleDrivers().Where(vd => vd.DriverId == driver.DriverId).ToList();

            _drivers.Add(driver);
        }

        public static void UpdateDriver(int driverId, Driver updatedDriver)
        {
            var driverToUpdate = _drivers.FirstOrDefault(x => x.DriverId == driverId);
            if (driverToUpdate != null)
            {
                driverToUpdate.Name = updatedDriver.Name;
                driverToUpdate.LicenseNumber = updatedDriver.LicenseNumber;

            }
        }

        public static void DeleteDriver(int driverId)
        {
            var driver = _drivers.FirstOrDefault(x => x.DriverId == driverId);
            if (driver != null)
            {
                _drivers.Remove(driver);
          
            }
        }
    }
}
