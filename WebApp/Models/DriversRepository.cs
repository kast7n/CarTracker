namespace WebApp.Models
{
    public class DriversRepository
    {
        private static List<Driver> _drivers = new List<Driver>()
        {
                   new Driver
                {
                    DriverId = 1,
                    VehicleId = 1,
                    DriverName = "John Doe",
                    StartDate = new DateTime(2023, 1, 15),
                    EndDate = new DateTime(2023, 6, 15)
                },
                new Driver
                {
                    DriverId = 2,
                    VehicleId = 1,
                    DriverName = "Jane Smith",
                    StartDate = new DateTime(2023, 6, 16),
                    EndDate = null // Current driver
                },
                new Driver
                {
                    DriverId = 3,
                    VehicleId = 2,
                    DriverName = "Bob Johnson",
                    StartDate = new DateTime(2023, 3, 20),
                    EndDate = new DateTime(2023, 9, 20)
                }
        };


        public static void AddDriver(Driver driver)
        {
            if (_drivers != null && _drivers.Count > 0)
            {
                var maxId = _drivers.Max(x => x.DriverId);
                driver.DriverId = maxId + 1;
            }
            else
            {
                driver.DriverId = 1;
            }
            if (_drivers == null)
            {
                _drivers = new List<Driver>();
            }
            _drivers.Add(driver);
        }

        public static List<Driver> GetDrivers() => _drivers;

        public static Driver? GetDriverById(int driverId)
        {
            var driver = _drivers.FirstOrDefault(x => x.DriverId == driverId);
            if (driver != null)
            {
                return new Driver
                {
                    DriverId = driver.DriverId,
                    StartDate = driver.StartDate,
                    EndDate = driver.EndDate,
                    DriverName = driver.DriverName,
                    VehicleId = driver.VehicleId,
                    Vehicle = driver.Vehicle
                };
            }
            return null;
        }

        public static List<Driver> GetDriversHistoryByVehicleId(int vehicleId)
        {
            var driver = _drivers.Where(x => x.VehicleId == vehicleId);
            if (driver != null)
            {
                return driver.ToList();
            }
            else
            {
                return new List<Driver>();
            }
        }


        public static void UpdateDriver(int DriverId, Driver driver)
        {
            if (DriverId != driver.DriverId) return;
            var DriverToUpdate = _drivers.FirstOrDefault(x => x.DriverId == DriverId);
            if (DriverToUpdate != null)
            {
                DriverToUpdate.DriverName = driver.DriverName;
                DriverToUpdate.StartDate = driver.StartDate;
                DriverToUpdate.EndDate = driver.EndDate;
            }
        }

        public static void DeleteDriver(int DriverId)
        {
            var driver = _drivers.FirstOrDefault(x => x.DriverId == DriverId);
            if (driver != null)
            {
                _drivers.Remove(driver);
            }
        }
    }
}
