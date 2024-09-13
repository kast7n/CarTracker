using Microsoft.EntityFrameworkCore;

namespace WebApp.Models
{
    public class VehicleInfoManagerContext : DbContext
    {
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleType> VehicleTypes { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<VehicleDriver> VehicleDrivers { get; set; }
        public DbSet<Maintenance> Maintenances { get; set; }

        public VehicleInfoManagerContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.HasKey(v => v.VehicleId);

                entity.Property(v => v.VehicleModel).IsRequired().HasMaxLength(100);

                entity.Property(v => v.Color).IsRequired().HasMaxLength(30);

                entity.Property(v => v.NumberOfSeats).IsRequired();

                entity.HasIndex(v => v.PlateNumber)
                      .IsUnique();

                entity.HasOne(v => v.Manufacturer)
                      .WithMany(m => m.Vehicles)
                      .HasForeignKey(v => v.ManufacturerId);

                entity.HasOne(v => v.Type)     
                      .WithMany(vt => vt.Vehicles)
                      .HasForeignKey(v => v.TypeId);

                entity.HasMany(v => v.VehicleDrivers)
                      .WithOne(vd => vd.Vehicle)
                      .HasForeignKey(vd => vd.VehicleId);

                entity.HasMany(v => v.Maintenances)
                      .WithOne(m => m.Vehicle)
                      .HasForeignKey(m => m.VehicleId);

            });

            modelBuilder.Entity<VehicleType>(entity =>
            {
                entity.HasKey(vt => vt.TypeId);

                entity.HasMany(vt => vt.Vehicles)
                      .WithOne(v => v.Type)
                      .HasForeignKey(v => v.TypeId);
            });

            modelBuilder.Entity<Manufacturer>(entity =>
            {
                entity.HasKey(m => m.ManufacturerId);

                entity.HasMany(m => m.Vehicles)
                      .WithOne(v => v.Manufacturer)
                      .HasForeignKey(v => v.ManufacturerId);
            });

            modelBuilder.Entity<Maintenance>(entity =>
            {
                entity.HasKey(entity => entity.MaintenanceId);

                entity.HasOne(m => m.Vehicle)
                      .WithMany(v => v.Maintenances)
                      .HasForeignKey(m => m.VehicleId);

            });

            modelBuilder.Entity<VehicleDriver>(entity =>
            {
                entity.HasKey(vd => vd.VehicleDriverId);



                entity.HasOne(vd => vd.Vehicle)
                      .WithMany(v => v.VehicleDrivers)
                      .HasForeignKey(vd => vd.VehicleId);


                entity.HasOne(vd => vd.Driver)
                      .WithMany(d => d.VehicleDrivers)
                      .HasForeignKey(vd => vd.DriverId);
                //Multiple VehicleDrivers can have same Vehicle need to add a constraint that they can't have same vehicle on same date/overlapping date

            });

            modelBuilder.Entity<Driver>(entity =>
            {

                entity.HasKey(d => d.DriverId);
                

                entity.HasIndex(d => d.LicenseNumber)
                      .IsUnique();

                
                entity.HasMany(d => d.VehicleDrivers)
                      .WithOne(vd => vd.Driver)
                      .HasForeignKey(vd => vd.DriverId);

            });
        }
    }
}
