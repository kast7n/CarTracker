﻿using Microsoft.EntityFrameworkCore;
using System;
using WebApp.Data;
using WebApp.Models;
using WebApp.Repositories.Interfaces;

namespace WebApp.Repositories
{
    public class VehicleDriversRepository : IVehicleDriverRepository
    {
        private readonly VehicleInfoManagerContext db;

        public VehicleDriversRepository(VehicleInfoManagerContext db)
        {
            this.db = db;
        }
        public void Delete(int vehicleDriverId)
        {
            var vd = db.VehicleDrivers.Find(vehicleDriverId);
            if (vd == null) return;
            db.VehicleDrivers.Remove(vd);
            db.SaveChanges();
        }

        public VehicleDriver? GetVehicleDriver(int vehicleDriverId)
        {
            return db.VehicleDrivers.FirstOrDefault(x => x.VehicleDriverId == vehicleDriverId);
        }

        public IEnumerable<VehicleDriver> GetVehicleDrivers(bool loadInfo = false)
        {
            if (loadInfo)
            {
                return db.VehicleDrivers.Include(x => x.Vehicle ).Include(x => x.Driver).OrderBy(x => x.VehicleDriverId).ToList();
            }
            return db.VehicleDrivers.OrderBy(x => x.VehicleDriverId).ToList();
        }

        public IEnumerable<VehicleDriver> GetVehicleDriversByDriverId(int driverId)
        {
            return db.VehicleDrivers.Where(v => v.DriverId == driverId).Include(x => x.Vehicle).ToList();
        }

        public IEnumerable<VehicleDriver> GetVehicleDriversByVehicleId(int vehicleId, bool loadInfo = false)
        {
            if (loadInfo)
            {
                return db.VehicleDrivers.Include(x => x.Driver ).Include(x => x.Vehicle).Where(x => x.VehicleId == vehicleId).OrderBy(x => x.VehicleDriverId).ToList();
            }
            else
            {
                return db.VehicleDrivers.Where(v => v.VehicleId == vehicleId).ToList();
            }
        }

        public void Insert(VehicleDriver vehicleDriver)
        {
            db.VehicleDrivers.Add(vehicleDriver);
            db.SaveChanges();
        }

        public void Update(int vehicleDriverId, VehicleDriver updatedVehicleDriver)
        {
            if (vehicleDriverId != updatedVehicleDriver.VehicleDriverId) return;

            var vd = db.VehicleDrivers.Find(vehicleDriverId);
            if (vd == null) return;


            vd.Vehicle = updatedVehicleDriver.Vehicle;
            vd.VehicleId = updatedVehicleDriver.VehicleId;
            vd.DriverId = updatedVehicleDriver.DriverId;
            vd.StartDate = updatedVehicleDriver.StartDate;
            vd.EndDate = updatedVehicleDriver.EndDate;
            vd.Driver = updatedVehicleDriver.Driver;
           
            db.SaveChanges();
        }
    }
}
