

namespace WebApp.Models
{
    public static class VehicleTypeRepository
    {
        private static List<VehicleType> _vehicleTypes = new List<VehicleType>()
        {
            new VehicleType { TypeId = 1, TypeName = "Sedan",Description="Sedan kinda thing" },
            new VehicleType { TypeId = 2, TypeName = "SUV",Description="SUV kinda thing" },
            new VehicleType { TypeId = 3, TypeName = "Truck",Description="Truck kinda thing" }
        };

        public static void AddVehicleType(VehicleType vehicleType)
        {
            if (_vehicleTypes != null && _vehicleTypes.Count > 0)
            {
                var maxId = _vehicleTypes.Max(x => x.TypeId);
                vehicleType.TypeId = maxId + 1;
            }
            else
            {
                vehicleType.TypeId = 1;
            }

            if (_vehicleTypes == null)
            {
                _vehicleTypes = new List<VehicleType>();
            }

            _vehicleTypes.Add(vehicleType);
        }

        public static List<VehicleType> GetVehicleTypes() => _vehicleTypes;

        public static VehicleType? GetVehicleTypeById(int typeId)
        {
            return _vehicleTypes.FirstOrDefault(x => x.TypeId == typeId);
        }

        public static void UpdateVehicleType(int typeId, VehicleType vehicleType)
        {
            if (typeId != vehicleType.TypeId) return;

            var vehicleTypeToUpdate = _vehicleTypes.FirstOrDefault(x => x.TypeId == typeId);
            if (vehicleTypeToUpdate != null)
            {
                vehicleTypeToUpdate.TypeName = vehicleType.TypeName;
                vehicleTypeToUpdate.Description = vehicleType.Description;
            }
        }

        public static void DeleteVehicleType(int typeId)
        {
            var vehicleType = _vehicleTypes.FirstOrDefault(x => x.TypeId == typeId);
            if (vehicleType != null)
            {
                _vehicleTypes.Remove(vehicleType);
            }
        }
    }
}
