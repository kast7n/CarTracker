using WebApp.Models;

namespace WebApp.Repositories.Interfaces
{
    public interface IManufacturerRepository
    {
        IEnumerable<Manufacturer> GetManufacturers();
        Manufacturer? GetManufacturerById(int manufacturerId);
        void Insert(Manufacturer manufacturer);
        void Update(int manufacturerId, Manufacturer updatedManufacturer);
        void Delete(int manufacturerId);
    }
}
