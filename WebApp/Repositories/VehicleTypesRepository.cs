using WebApp.Data;
using WebApp.Models;
using WebApp.Repositories.Interfaces;

namespace WebApp.Repositories
{
    public class VehicleTypesRepository : IVehicleTypeRepository
    {
        private readonly VehicleInfoManagerContext db;
        public VehicleTypesRepository(VehicleInfoManagerContext db)
        {
            this.db = db;
        }
        public void Delete(int typeId)
        {
            var type = db.VehicleTypes.Find(typeId);
            if (type == null) return;
            db.VehicleTypes.Remove(type);
            db.SaveChanges();
        }

        public VehicleType? GetVehicleTypeById(int typeId)
        {
            return db.VehicleTypes.FirstOrDefault(x => x.TypeId == typeId);
        }

        public IEnumerable<VehicleType> GetVehicleTypes()
        {
            return db.VehicleTypes.OrderBy(x => x.TypeId).ToList();
        }

        public void Insert(VehicleType vehicleType)
        {
            db.VehicleTypes.Add(vehicleType);
            db.SaveChanges();
        }

        public void Update(int typeId, VehicleType updatedVehicleType)
        {
            if (typeId != updatedVehicleType.TypeId) return;

            var type = db.VehicleTypes.Find(typeId);
            if (type == null) return;

            type.TypeName = updatedVehicleType.TypeName;
            type.Description = updatedVehicleType.Description;

            db.SaveChanges();
        }
    }
}