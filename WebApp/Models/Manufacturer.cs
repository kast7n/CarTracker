namespace WebApp.Models
{
    public class Manufacturer
    {
        public int ManufacturerId { get; set; }
        public string? ManufacturerName { get; set; }
        public string? Website {  get; set; }

        public List<Vehicle>? Vehicles { get; set; }


    }
}
