using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.ViewModels
{
    public class VehicleInfoTrackerContext : DbContext
    {
        public VehicleInfoTrackerContext(DbContextOptions<VehicleInfoTrackerContext> options) : base(options)
        {

        }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Maintenance> Maintenances { get; set; }
        public DbSet<VehicleDriver> VehicleDrivers { get; set; }


    }
}
