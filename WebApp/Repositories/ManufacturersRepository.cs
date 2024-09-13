using WebApp.Data;
using WebApp.Models;
using WebApp.Repositories.Interfaces;

namespace WebApp.Repositories
{
    public class ManufacturersRepository : IManufacturerRepository
    {
        private readonly VehicleInfoManagerContext db;
        public ManufacturersRepository(VehicleInfoManagerContext db)
        {
            this.db = db;
        }
        public void Delete(int manufacturerId)
        {
            var manu = db.Manufacturers.Find(manufacturerId);
            if (manu == null) return;
            db.Manufacturers.Remove(manu);
            db.SaveChanges();
        }

        public Manufacturer? GetManufacturerById(int manufacturerId)
        {
            return db.Manufacturers.FirstOrDefault(x => x.ManufacturerId == manufacturerId);
        }

        public IEnumerable<Manufacturer> GetManufacturers()
        {
            return db.Manufacturers.OrderBy(x => x.ManufacturerId).ToList();
        }

        public void Insert(Manufacturer manufacturer)
        {
            db.Manufacturers.Add(manufacturer);
            db.SaveChanges();
        }

        public void Update(int manufacturerId, Manufacturer updatedManufacturer)
        {
            if (manufacturerId != updatedManufacturer.ManufacturerId) return;

            var manu = db.Manufacturers.Find(manufacturerId);
            if (manu == null) return;
            
            manu.ManufacturerName = updatedManufacturer.ManufacturerName;
            manu.Website = updatedManufacturer.Website;
            db.SaveChanges();
        }
    }
}
