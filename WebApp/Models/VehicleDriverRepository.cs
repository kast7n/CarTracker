

namespace WebApp.Models
{
    public static class VehicleDriverRepository
    {
        // Sample in-memory storage for VehicleDriver assignments
        private static List<VehicleDriver> _vehicleDrivers = new List<VehicleDriver>()
        {
            new VehicleDriver { AssignmentId = 1, DriverId = 1,DriverName = DriversRepository.GetDriverById(1)?.Name,  VehicleId = 1, StartDate = new DateTime(2023, 1, 1), EndDate = null },
            new VehicleDriver { AssignmentId = 2, DriverId = 1, DriverName = DriversRepository.GetDriverById(1)?.Name, VehicleId = 1, StartDate = new DateTime(2023, 6, 1), EndDate = new DateTime(2023, 12, 1) },
            new VehicleDriver { AssignmentId = 3, DriverId = 2, DriverName = DriversRepository.GetDriverById(2)?.Name, VehicleId = 2, StartDate = new DateTime(2023, 2, 1), EndDate = new DateTime(2023, 8, 1) },
            new VehicleDriver { AssignmentId = 4, DriverId = 1, DriverName = DriversRepository.GetDriverById(1)?.Name, VehicleId = 3, StartDate = new DateTime(2023, 3, 1), EndDate = null }
        };

    

        public static void AddVehicleDriver(VehicleDriver vehicleDriver)
        {
            // Ensure the unique AssignmentId is set (typically handled by the database)
            if (vehicleDriver.AssignmentId == 0)
            {
                vehicleDriver.AssignmentId = _vehicleDrivers.Max(vd => vd.AssignmentId) + 1;
            }
            vehicleDriver.Vehicle = VehiclesRepository.GetVehicleById(vehicleDriver.VehicleId);
            vehicleDriver.Driver = DriversRepository.GetDriverById(vehicleDriver.DriverId);

            _vehicleDrivers.Add(vehicleDriver);
        }

        public static List<VehicleDriver> GetVehicleDrivers() => _vehicleDrivers;

        public static VehicleDriver? GetVehicleDriver(int assignmentId)
        {
            return _vehicleDrivers.FirstOrDefault(vd => vd.AssignmentId == assignmentId);
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
            var vehicleDriverToUpdate = _vehicleDrivers.FirstOrDefault(vd => vd.AssignmentId == assignmentId);
            if (vehicleDriverToUpdate != null)
            {
                vehicleDriverToUpdate.StartDate = updatedVehicleDriver.StartDate;
                vehicleDriverToUpdate.EndDate = updatedVehicleDriver.EndDate;
            }
        }

        public static void DeleteVehicleDriver(int assignmentId)
        {
            var vehicleDriver = _vehicleDrivers.FirstOrDefault(vd => vd.AssignmentId == assignmentId);
            if (vehicleDriver != null)
            {
                _vehicleDrivers.Remove(vehicleDriver);
            }
        }
    }
}
