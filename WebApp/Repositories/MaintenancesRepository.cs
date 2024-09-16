using Microsoft.EntityFrameworkCore;
using System;
using WebApp.Data;
using WebApp.Models;
using WebApp.Repositories.Interfaces;

namespace WebApp.Repositories
{
    public class MaintenancesRepository : IMaintenanceRepository
    {
        private readonly VehicleInfoManagerContext db;
        public MaintenancesRepository(VehicleInfoManagerContext db)
        {
            this.db = db;
        }
        public void Delete(int maintenanceId)
        {
            var maint = db.Maintenances.Find(maintenanceId);
            if (maint == null) return;
            db.Maintenances.Remove(maint);
            db.SaveChanges();
        }

        public Maintenance? GetMaintenanceById(int maintenanceId)
        {
            return db.Maintenances.FirstOrDefault(x => x.MaintenanceId == maintenanceId);
        }

        public IEnumerable<Maintenance> GetMaintenanceByVehicleId(int vehicleId)
        {
            return db.Maintenances.Include(x => x.Vehicle).Where(v => v.VehicleId == vehicleId).ToList();
        }

        public IEnumerable<Maintenance> GetMaintenances()
        {
            return db.Maintenances.Include(x => x.Vehicle).OrderBy(x => x.MaintenanceId).ToList();
        }

        public void Insert(Maintenance maintenance)
        {
            db.Maintenances.Add(maintenance);
            db.SaveChanges();
        }

        public void Update(int maintenanceId, Maintenance updatedMaintenance)
        {
            if (maintenanceId != updatedMaintenance.MaintenanceId) return;

            var maint = db.Maintenances.Find(maintenanceId);
            if (maint == null) return;

            maint.Vehicle = updatedMaintenance.Vehicle;
            maint.VehicleId = updatedMaintenance.VehicleId;
            maint.MaintenanceType = updatedMaintenance.MaintenanceType;
            maint.MaintenanceDate = updatedMaintenance.MaintenanceDate;
            maint.Description = updatedMaintenance.Description;
            db.SaveChanges();
        }
    }
}
