namespace WebApp.Models
{
    public class VehiclesRepository
    {
        private static List<Vehicle> _vehicles = new List<Vehicle>()
        {
            new Vehicle {     VehicleId = 1,     VehicleModel = "Model S",     PlateNumber = "ABC123",     NumberOfSeats = 5,     Color = "Red",     Manufacturer = "Tesla" },
            new Vehicle {     VehicleId = 2,     VehicleModel = "Accord",     PlateNumber = "XYZ456",     NumberOfSeats = 4,     Color = "Blue",     Manufacturer = "Honda" },
            new Vehicle {     VehicleId = 3,     VehicleModel = "F-150",     PlateNumber = "LMN789",     NumberOfSeats = 6,     Color = "Black",     Manufacturer = "Ford" }
        };


        public static void AddVehicle(Vehicle vehicle)
        {
            if (_vehicles != null && _vehicles.Count > 0)
            {
                var maxId = _vehicles.Max(x => x.VehicleId);
                vehicle.VehicleId = maxId + 1;
            }
            else
            {
                vehicle.VehicleId = 1;
            }
            if (_vehicles == null)
            {
                _vehicles = new List<Vehicle>();
            }
            _vehicles.Add(vehicle);
        }

        public static List<Vehicle> GetVehicles() => _vehicles;

        public static Vehicle? GetVehicleById(int vehicleId)
        {
            var vehicle = _vehicles.FirstOrDefault(x => x.VehicleId == vehicleId);
            if(vehicle != null)
            {
                return new Vehicle
                {
                    VehicleId = vehicle.VehicleId,
                    VehicleModel = vehicle.VehicleModel,
                    PlateNumber = vehicle.PlateNumber,
                    Color = vehicle.Color,
                    Manufacturer = vehicle.Manufacturer,
                    NumberOfSeats = vehicle.NumberOfSeats
                };
            }
            return null;
        }


        public static void UpdateVehicle(int vehicleId, Vehicle vehicle)
        {
            if (vehicleId != vehicle.VehicleId) return;
            var vehicleToUpdate = _vehicles.FirstOrDefault(x => x.VehicleId == vehicleId);
            if (vehicleToUpdate != null)
            {
                vehicleToUpdate.VehicleModel = vehicle.VehicleModel;
                vehicleToUpdate.PlateNumber = vehicle.PlateNumber;
                vehicleToUpdate.Color = vehicle.Color;
                vehicleToUpdate.Manufacturer = vehicle.Manufacturer;
                vehicleToUpdate.NumberOfSeats = vehicle.NumberOfSeats;
            }
        }

        public static void DeleteVehicle(int vehicleId)
        {
            var vehicle = _vehicles.FirstOrDefault(x => x.VehicleId == vehicleId);
            if( vehicle != null)
            {
                _vehicles.Remove(vehicle);
            }
        }

    }
}
