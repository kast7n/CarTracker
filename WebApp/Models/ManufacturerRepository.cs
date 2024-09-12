using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApp.Models
{
    public static class VehicleManufacturerRepository
    {
        private static List<Manufacturer> _manufacturers = new List<Manufacturer>();

        static VehicleManufacturerRepository()
        {
            // Initialize with some manufacturers for demonstration
            var tesla = new Manufacturer { ManufacturerId = 1, ManufacturerName = "Tesla", Website = "https://www.tesla.com" };
            var honda = new Manufacturer { ManufacturerId = 2, ManufacturerName = "Honda", Website = "https://www.honda.com" };
            var ford = new Manufacturer { ManufacturerId = 3, ManufacturerName = "Ford", Website = "https://www.ford.com" };

            _manufacturers.AddRange(new[] { tesla, honda, ford });
        }

        public static List<Manufacturer> GetManufacturers() => _manufacturers;

        public static Manufacturer? GetManufacturerById(int manufacturerId)
        {
            return _manufacturers.FirstOrDefault(x => x.ManufacturerId == manufacturerId);
        }

        public static void AddManufacturer(Manufacturer manufacturer)
        {
            if (manufacturer.ManufacturerId == 0)
            {
                manufacturer.ManufacturerId = _manufacturers.Any() ? _manufacturers.Max(x => x.ManufacturerId) + 1 : 1;
            }

            manufacturer.Vehicles = new List<Vehicle>();
            _manufacturers.Add(manufacturer);
        }

        public static void UpdateManufacturer(int manufacturerId, Manufacturer updatedManufacturer)
        {
            var manufacturerToUpdate = _manufacturers.FirstOrDefault(x => x.ManufacturerId == manufacturerId);
            if (manufacturerToUpdate != null)
            {
                manufacturerToUpdate.ManufacturerName = updatedManufacturer.ManufacturerName;
                manufacturerToUpdate.Website = updatedManufacturer.Website;

            }
        }

        public static void DeleteManufacturer(int manufacturerId)
        {
            var manufacturer = _manufacturers.FirstOrDefault(x => x.ManufacturerId == manufacturerId);
            if (manufacturer != null)
            {
                // Remove associated vehicles from VehiclesRepository
                var vehicles = VehiclesRepository.GetVehiclesByManufacturerId(manufacturerId);
                var vehiclesToRemove = vehicles.Where(v => v.Manufacturer?.ManufacturerId == manufacturer.ManufacturerId).ToList();

                foreach (var vehicle in vehiclesToRemove)
                {
                    VehiclesRepository.DeleteVehicle(vehicle.VehicleId);
                }

                _manufacturers.Remove(manufacturer);
            }
        }
    }
}