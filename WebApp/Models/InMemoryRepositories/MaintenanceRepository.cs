using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApp.Models.InMemoryRepositories
{
    public static class MaintenanceRepository
    {
        private static List<Maintenance> _maintenanceHistory = new List<Maintenance>();

        static MaintenanceRepository()
        {
            // Initialize with some sample data
            _maintenanceHistory.AddRange(new[]
            {
                new Maintenance
                {
                    MaintenanceId = 1,
                    VehicleId = 1,
                    MaintenanceType = "Oil Change",
                    MaintenanceDate = new DateTime(2024, 1, 15),
                    Description = "Changed engine oil and filter.",
                    Vehicle = VehiclesRepository.GetVehicleById(1)
                },
                new Maintenance
                {
                    MaintenanceId = 2,
                    VehicleId = 1,
                    MaintenanceType = "Tire Rotation",
                    MaintenanceDate = new DateTime(2024, 2, 20),
                    Description = "Rotated tires to ensure even wear.",
                    Vehicle = VehiclesRepository.GetVehicleById(1)
                },
                new Maintenance
                {
                    MaintenanceId = 3,
                    VehicleId = 2,
                    MaintenanceType = "Brake Inspection",
                    MaintenanceDate = new DateTime(2024, 3, 10),
                    Description = "Inspected and replaced brake pads.",
                    Vehicle = VehiclesRepository.GetVehicleById(2)
                }
            });
        }

        public static List<Maintenance> GetMaintenanceHistory() => _maintenanceHistory;

        public static Maintenance? GetMaintenanceById(int maintenanceId)
        {
            var maintenance = _maintenanceHistory.FirstOrDefault(x => x.MaintenanceId == maintenanceId);
            return maintenance != null ? CloneMaintenance(maintenance) : null;
        }

        public static List<Maintenance> GetMaintenanceByVehicleId(int vehicleId)
        {
            return _maintenanceHistory
                .Where(x => x.VehicleId == vehicleId)
                .Select(CloneMaintenance)
                .ToList();
        }

        public static void AddMaintenance(Maintenance maintenance)
        {
            if (maintenance.MaintenanceId == 0)
            {
                maintenance.MaintenanceId = _maintenanceHistory.Any() ? _maintenanceHistory.Max(x => x.MaintenanceId) + 1 : 1;
            }

            // Update vehicle reference
            var vehicle = VehiclesRepository.GetVehicleById(maintenance.VehicleId);
            maintenance.Vehicle = vehicle;

            _maintenanceHistory.Add(maintenance);
        }

        public static void UpdateMaintenance(int maintenanceId, Maintenance updatedMaintenance)
        {
            var maintenanceToUpdate = _maintenanceHistory.FirstOrDefault(x => x.MaintenanceId == maintenanceId);
            if (maintenanceToUpdate != null)
            {
                maintenanceToUpdate.MaintenanceType = updatedMaintenance.MaintenanceType;
                maintenanceToUpdate.MaintenanceDate = updatedMaintenance.MaintenanceDate;
                maintenanceToUpdate.Description = updatedMaintenance.Description;
            }
        }

        public static void DeleteMaintenance(int maintenanceId)
        {
            var maintenance = _maintenanceHistory.FirstOrDefault(x => x.MaintenanceId == maintenanceId);
            if (maintenance != null)
            {
                _maintenanceHistory.Remove(maintenance);
            }
        }

        private static Maintenance CloneMaintenance(Maintenance maintenance)
        {
            return new Maintenance
            {
                MaintenanceId = maintenance.MaintenanceId,
                VehicleId = maintenance.VehicleId,
                MaintenanceType = maintenance.MaintenanceType,
                MaintenanceDate = maintenance.MaintenanceDate,
                Description = maintenance.Description,
                Vehicle = maintenance.Vehicle
            };
        }
    }
}
