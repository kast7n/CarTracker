namespace WebApp.Models
{
    public class MaintenanceRepository
    {
        private static List<Maintenance> _maintenanceHistory = new List<Maintenance>()
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
        };


        public static void AddMaintenanceHistory(Maintenance maintenance)
        {
            if (_maintenanceHistory != null && _maintenanceHistory.Count > 0)
            {
                var maxId = _maintenanceHistory.Max(x => x.MaintenanceId);
                maintenance.MaintenanceId = maxId + 1;
            }
            else
            {
                maintenance.MaintenanceId = 1;
            }
            if (_maintenanceHistory == null)
            {
                _maintenanceHistory = new List<Maintenance>();
            }
            _maintenanceHistory.Add(maintenance);
        }

        public static List<Maintenance> GetMaintenanceHistory() => _maintenanceHistory;

        public static Maintenance? GetMaintenanceHistoryById(int maintenanceHistoryId)
        {
            var maintenance = _maintenanceHistory.FirstOrDefault(x => x.MaintenanceId == maintenanceHistoryId);
            if (maintenance != null)
            {
                return new Maintenance
                {
                    MaintenanceId = maintenance.MaintenanceId,
                    MaintenanceDate = maintenance.MaintenanceDate,
                    MaintenanceType = maintenance.MaintenanceType,
                    Description = maintenance.Description,
                    VehicleId = maintenance.VehicleId,
                    Vehicle = maintenance.Vehicle
                };
            }
            return null;
        }

        public static List<Maintenance> GetMaintenanceHistoryByVehicleId(int vehicleId)
        {
            var maintenance = _maintenanceHistory.Where(x => x.VehicleId == vehicleId);
            if (maintenance != null)
            {
                return maintenance.ToList();
            }
            else
            {
                return new List<Maintenance>();
            }
        }


        public static void UpdateMaintenanceHistory(int maintenanceHistoryId, Maintenance maintenance)
        {
            if (maintenanceHistoryId != maintenance.MaintenanceId) return;
            var maintenanceToUpdate = _maintenanceHistory.FirstOrDefault(x => x.MaintenanceId == maintenanceHistoryId);
            if (maintenanceToUpdate != null)
            {
                maintenanceToUpdate.MaintenanceType = maintenance.MaintenanceType;
                maintenanceToUpdate.MaintenanceDate = maintenance.MaintenanceDate;
                maintenanceToUpdate.Description = maintenance.Description;
            }
        }

        public static void DeleteMaintenanceHistory(int maintenanceHistoryId)
        {
            var maintenance = _maintenanceHistory.FirstOrDefault(x => x.MaintenanceId == maintenanceHistoryId);
            if (maintenance != null)
            {
                _maintenanceHistory.Remove(maintenance);
            }
        }

    }
}

