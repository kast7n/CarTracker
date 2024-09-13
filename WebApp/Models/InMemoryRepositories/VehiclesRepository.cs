using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApp.Models.InMemoryRepositories
{
    public static class VehiclesRepository
    {
        private static List<Vehicle> _vehicles = new List<Vehicle>();

        static VehiclesRepository()
        {


            _vehicles.AddRange(new[]
            {
                new Vehicle { VehicleId = 1, VehicleModel = "Model S", PlateNumber = "ABC123", NumberOfSeats = 5, Color = "Red", ManufacturerId = 1 ,TypeId = 2},
                new Vehicle { VehicleId = 2, VehicleModel = "Accord", PlateNumber = "XYZ456", NumberOfSeats = 4, Color = "Blue", ManufacturerId = 2 ,TypeId = 1},
                new Vehicle { VehicleId = 3, VehicleModel = "F-150", PlateNumber = "LMN789", NumberOfSeats = 6, Color = "Black", ManufacturerId = 3 ,TypeId = 3}
            });


        }

        public static List<Vehicle> GetVehicles(bool loadInfo = false)
        {
            if (!loadInfo) return _vehicles;
            else
            {
                if (_vehicles.Count > 0 && _vehicles != null)
                {
                    _vehicles.ForEach(x =>
                    {
                        if (x.TypeId.HasValue)
                        {
                            x.Type = VehicleTypeRepository.GetVehicleTypeById(x.TypeId.Value);
                        }
                        if (x.ManufacturerId.HasValue)
                        {
                            x.Manufacturer = ManufacturerRepository.GetManufacturerById(x.ManufacturerId.Value);
                        }
                    });
                }
                return _vehicles ?? new List<Vehicle>();
            }
        }
        public static Vehicle? GetVehicleById(int vehicleId, bool loadInfo = false)
        {
            var vehicle = _vehicles.FirstOrDefault(x => x.VehicleId == vehicleId);
            if (vehicle != null)
            {
                var veh = new Vehicle
                {
                    VehicleId = vehicle.VehicleId,
                    TypeId = vehicle.TypeId,
                    ManufacturerId = vehicle.ManufacturerId,
                    Color = vehicle.Color,
                    VehicleModel = vehicle.VehicleModel,
                    NumberOfSeats = vehicle.NumberOfSeats,
                    PlateNumber = vehicle.PlateNumber
                };
                if (loadInfo && veh.ManufacturerId.HasValue && veh.TypeId.HasValue)
                {
                    veh.Manufacturer = ManufacturerRepository.GetManufacturerById(veh.ManufacturerId.Value);
                    veh.Type = VehicleTypeRepository.GetVehicleTypeById(veh.TypeId.Value);
                }
                return veh;
            }
            return null;
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
                vehicleToUpdate.ManufacturerId = updatedVehicle.ManufacturerId;
                vehicleToUpdate.TypeId = updatedVehicle.TypeId;
                vehicleToUpdate.Manufacturer = updatedVehicle.Manufacturer;
                vehicleToUpdate.Type = updatedVehicle.Type;


            }
        }

        public static void DeleteVehicle(int vehicleId)
        {
            var vehicle = _vehicles.FirstOrDefault(x => x.VehicleId == vehicleId);
            if (vehicle != null)
            {
                if (vehicle.Manufacturer != null)
                {
                    var manufacturer = ManufacturerRepository.GetManufacturerById(vehicle.Manufacturer.ManufacturerId);

                }

                _vehicles.Remove(vehicle);
            }
        }

        public static List<Vehicle> GetVehiclesByManufacturerId(int manufacturerId)
        {
            return _vehicles.Where(v => v.ManufacturerId == manufacturerId).ToList();
        }

        public static List<Vehicle> GetVehiclesByDriverId(int driverId)
        {
            var vehicleDrivers = VehicleDriversRepository.GetVehicleDrivers();

            var vehicleIds = vehicleDrivers
                .Where(vd => vd.DriverId == driverId)
                .Select(vd => vd.VehicleId)
                .ToList();

            return _vehicles.Where(v => vehicleIds.Contains(v.VehicleId)).ToList();
        }
    }
}
