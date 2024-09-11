namespace WebApp.Models
{
    public class DriversRepository
    {
        private static List<Driver> _Drivers = new List<Driver>()
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
            if (_Drivers != null && _Drivers.Count > 0)
            {
                var maxId = _Drivers.Max(x => x.DriverId);
                driver.DriverId = maxId + 1;
            }
            else
            {
                driver.DriverId = 1;
            }
            if (_Drivers == null)
            {
                _Drivers = new List<Driver>();
            }
            _Drivers.Add(driver);
        }

        public static List<Driver> GetDriver() => _Drivers;

        public static Driver? GetDriverById(int DriverId)
        {
            var driver = _Drivers.FirstOrDefault(x => x.DriverId == DriverId);
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
            var driver = _Drivers.Where(x => x.VehicleId == vehicleId);
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
            var DriverToUpdate = _Drivers.FirstOrDefault(x => x.DriverId == DriverId);
            if (DriverToUpdate != null)
            {
                DriverToUpdate.DriverName = driver.DriverName;
                DriverToUpdate.StartDate = driver.StartDate;
                DriverToUpdate.EndDate = driver.EndDate;
            }
        }

        public static void DeleteDriver(int DriverId)
        {
            var driver = _Drivers.FirstOrDefault(x => x.DriverId == DriverId);
            if (driver != null)
            {
                _Drivers.Remove(driver);
            }
        }
    }
}
