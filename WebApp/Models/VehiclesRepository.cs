using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApp.Models
{
    public static class VehiclesRepository
    {
        private static List<Vehicle> _vehicles = new List<Vehicle>();

        static VehiclesRepository()
        {
            // Initialize with some vehicles for demonstration
            var tesla = VehicleManufacturerRepository.GetManufacturerById(1);
            var honda = VehicleManufacturerRepository.GetManufacturerById(2);
            var ford = VehicleManufacturerRepository.GetManufacturerById(3);

            _vehicles.AddRange(new[]
            {
                new Vehicle { VehicleId = 1, VehicleModel = "Model S", PlateNumber = "ABC123", NumberOfSeats = 5, Color = "Red", Manufacturer = tesla },
                new Vehicle { VehicleId = 2, VehicleModel = "Accord", PlateNumber = "XYZ456", NumberOfSeats = 4, Color = "Blue", Manufacturer = honda },
                new Vehicle { VehicleId = 3, VehicleModel = "F-150", PlateNumber = "LMN789", NumberOfSeats = 6, Color = "Black", Manufacturer = ford }
            });

 
        }

        public static List<Vehicle> GetVehicles() => _vehicles;

        public static Vehicle? GetVehicleById(int vehicleId)
        {
            return _vehicles.FirstOrDefault(x => x.VehicleId == vehicleId);
        }

        public static void AddVehicle(Vehicle vehicle)
        {
            if (vehicle.VehicleId == 0)
            {
                vehicle.VehicleId = _vehicles.Any() ? _vehicles.Max(x => x.VehicleId) + 1 : 1;
            }

            _vehicles.Add(vehicle);
        }

        public static void UpdateVehicle(int vehicleId, Vehicle updatedVehicle)
        {
            var vehicleToUpdate = _vehicles.FirstOrDefault(x => x.VehicleId == vehicleId);
            if (vehicleToUpdate != null)
            {
                vehicleToUpdate.VehicleModel = updatedVehicle.VehicleModel;
                vehicleToUpdate.PlateNumber = updatedVehicle.PlateNumber;
                vehicleToUpdate.NumberOfSeats = updatedVehicle.NumberOfSeats;
                vehicleToUpdate.Color = updatedVehicle.Color;

                if (updatedVehicle.Manufacturer != null)
                {
                    var manufacturer = VehicleManufacturerRepository.GetManufacturerById(updatedVehicle.Manufacturer.ManufacturerId);
                    if (manufacturer != null)
                    {
                        vehicleToUpdate.Manufacturer = manufacturer;
                    }
                }
            }
        }

        public static void DeleteVehicle(int vehicleId)
        {
            var vehicle = _vehicles.FirstOrDefault(x => x.VehicleId == vehicleId);
            if (vehicle != null)
            {
                if (vehicle.Manufacturer != null)
                {
                    var manufacturer = VehicleManufacturerRepository.GetManufacturerById(vehicle.Manufacturer.ManufacturerId);
             
                }

                _vehicles.Remove(vehicle);
            }
        }

        public static List<Vehicle> GetVehiclesByManufacturerId(int manufacturerId)
        {
            return _vehicles.Where(v => v.Manufacturer?.ManufacturerId == manufacturerId).ToList();
        }

        public static List<Vehicle> GetVehiclesByDriverId(int driverId)
        {
            var vehicleDrivers = VehicleDriverRepository.GetVehicleDrivers();

            var vehicleIds = vehicleDrivers
                .Where(vd => vd.DriverId == driverId)
                .Select(vd => vd.VehicleId)
                .ToList();

            return _vehicles.Where(v => vehicleIds.Contains(v.VehicleId)).ToList();
        }
    }
}
