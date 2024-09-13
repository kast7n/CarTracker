using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;
using WebApp.Repositories.Interfaces;

namespace WebApp.Repositories
{
    public class VehiclesRepository : IVehicleRepository
    {
        private readonly VehicleInfoManagerContext db;
        public VehiclesRepository(VehicleInfoManagerContext db)
        {
            this.db = db;
        }
        public void Delete(int vehicleId)
        {
            var veh = db.Vehicles.Find(vehicleId);
            if (veh == null) return;
            db.Vehicles.Remove(veh);
            db.SaveChanges();
        }

        public Vehicle? GetVehicleById(int vehicleId, bool loadInfo = false)
        {
            if (loadInfo)
            {
                return db.Vehicles.Include(x => x.Type).Include(x => x.Manufacturer).FirstOrDefault(x => x.VehicleId == vehicleId);
            }
            else
            {
                return db.Vehicles.FirstOrDefault(x => x.VehicleId == vehicleId);
            }
        }

        public IEnumerable<Vehicle> GetvehicleByTypeId(int typeId)
        {
            return db.Vehicles.Where(v => v.TypeId == typeId).ToList();
        }

        public IEnumerable<Vehicle> GetVehicles(bool loadInfo = false)
        {
            if (loadInfo)
            {
                return db.Vehicles.Include(x => x.Type).Include(x => x.Manufacturer).OrderBy(x => x.VehicleId).ToList();
            }
            else
            {
                return db.Vehicles.OrderBy(x => x.VehicleId).ToList();
            }
        }

        public IEnumerable<Vehicle> GetVehiclesByDriverId(int driverId)
        {
            return db.Vehicles.Where(v => v.VehicleDrivers != null && v.VehicleDrivers.Any(vd => vd.DriverId == driverId)).ToList();
        }

        public IEnumerable<Vehicle> GetVehiclesByManufacturerId(int manufacturerId)
        {
            return db.Vehicles.Where(v => v.ManufacturerId == manufacturerId).ToList();
        }

        public void Insert(Vehicle vehicle)
        {
            db.Vehicles.Add(vehicle);
            db.SaveChanges();
        }

        public void Update(int vehicleId, Vehicle updatedVehicle)
        {
            if (vehicleId != updatedVehicle.VehicleId) return;

            var veh = db.Vehicles.Find(vehicleId);
            if (veh == null) return;

            veh.VehicleModel = updatedVehicle.VehicleModel;
            veh.PlateNumber = updatedVehicle.PlateNumber;
            veh.NumberOfSeats = updatedVehicle.NumberOfSeats;
            veh.Color = updatedVehicle.Color;
            veh.ManufacturerId = updatedVehicle.ManufacturerId;
            veh.TypeId = updatedVehicle.TypeId;
            veh.Manufacturer = updatedVehicle.Manufacturer;
            veh.Type = updatedVehicle.Type;
            db.SaveChanges();
        }
    }
}
