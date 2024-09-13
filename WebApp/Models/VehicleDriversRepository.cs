

namespace WebApp.Models
{
    public static class VehicleDriversRepository
    {
        // Sample in-memory storage for VehicleDriver assignments
        private static List<VehicleDriver> _vehicleDrivers = new List<VehicleDriver>()
        {
            new VehicleDriver { VehicleDriverId = 1, DriverId = 1,DriverName = DriversRepository.GetDriverById(1)?.Name,  VehicleId = 1, StartDate = new DateTime(2023, 1, 1), EndDate = new DateTime(2023, 12, 2) },
            new VehicleDriver { VehicleDriverId = 2, DriverId = 1, DriverName = DriversRepository.GetDriverById(1)?.Name, VehicleId = 1, StartDate = new DateTime(2023, 6, 1), EndDate = new DateTime(2023, 12, 1) },
            new VehicleDriver { VehicleDriverId = 3, DriverId = 2, DriverName = DriversRepository.GetDriverById(2)?.Name, VehicleId = 2, StartDate = new DateTime(2023, 2, 1), EndDate = new DateTime(2023, 8, 3) },
            new VehicleDriver { VehicleDriverId = 4, DriverId = 1, DriverName = DriversRepository.GetDriverById(1)?.Name, VehicleId = 3, StartDate = new DateTime(2023, 3, 1), EndDate = new DateTime(2023, 12, 4) }
        };

    

        public static void AddVehicleDriver(VehicleDriver vehicleDriver)
        {
            // Ensure the unique VehicleDriverId is set (typically handled by the database)
            if (vehicleDriver.VehicleDriverId == 0)
            {
                vehicleDriver.VehicleDriverId = _vehicleDrivers.Max(vd => vd.VehicleDriverId) + 1;
            }
            vehicleDriver.Vehicle = VehiclesRepository.GetVehicleById(vehicleDriver.VehicleId);
            vehicleDriver.Driver = DriversRepository.GetDriverById(vehicleDriver.DriverId);
            vehicleDriver.DriverName = vehicleDriver.Driver?.Name;

            _vehicleDrivers.Add(vehicleDriver);
        }

        public static List<VehicleDriver> GetVehicleDrivers() => _vehicleDrivers;

        public static VehicleDriver? GetVehicleDriver(int assignmentId)
        {
            return _vehicleDrivers.FirstOrDefault(vd => vd.VehicleDriverId == assignmentId);
        }

        public static List<VehicleDriver> GetVehicleDriversByDriverId(int driverId)
        {
            return _vehicleDrivers.Where(vd => vd.DriverId == driverId).ToList();
        }

        public static List<VehicleDriver> GetVehicleDriversByVehicleId(int vehicleId)
        {
            return _vehicleDrivers.Where(vd => vd.VehicleId == vehicleId).ToList();
        }

        public static void UpdateVehicleDriver(int assignmentId, VehicleDriver updatedVehicleDriver)
        {
            var vehicleDriverToUpdate = _vehicleDrivers.FirstOrDefault(vd => vd.VehicleDriverId == assignmentId);
            if (vehicleDriverToUpdate != null)
            {
                vehicleDriverToUpdate.DriverId  = updatedVehicleDriver.DriverId;
                vehicleDriverToUpdate.DriverName = DriversRepository.GetDriverById(updatedVehicleDriver.DriverId)?.Name;
                vehicleDriverToUpdate.StartDate = updatedVehicleDriver.StartDate;
                vehicleDriverToUpdate.EndDate = updatedVehicleDriver.EndDate;
            }
        }

        public static void DeleteVehicleDriver(int assignmentId)
        {
            var vehicleDriver = _vehicleDrivers.FirstOrDefault(vd => vd.VehicleDriverId == assignmentId);
            if (vehicleDriver != null)
            {
                _vehicleDrivers.Remove(vehicleDriver);
            }
        }
    }
}
