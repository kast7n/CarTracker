using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class ManufacturersController : Controller
    {
        public IActionResult Index()
        {
            var Type = ManufacturerRepository.GetManufacturers();
            return View(Type);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            ViewBag.Action = "edit";
            var manufacturer = ManufacturerRepository.GetManufacturerById(id.HasValue ? id.Value : 0);
            return View(manufacturer);

        }

        [HttpPost]
        public IActionResult Edit(Manufacturer manufacturer)
        {
            if (ModelState.IsValid)
            {
                ManufacturerRepository.UpdateManufacturer(manufacturer.ManufacturerId, manufacturer);
                return RedirectToAction(nameof(Index));
            }

            return View(manufacturer);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "add";
            return View();
        }
        [HttpPost]
        public IActionResult Add(Manufacturer manufacturer)
        {

            if (ModelState.IsValid)
            {
                ManufacturerRepository.AddManufacturer(manufacturer);
                return RedirectToAction(nameof(Index));
            }

            return View(manufacturer);
        }

        public IActionResult Delete(int id)
        {
            ManufacturerRepository.DeleteManufacturer(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
