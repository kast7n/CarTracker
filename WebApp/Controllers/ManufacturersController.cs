using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Repositories.Interfaces;

namespace WebApp.Controllers
{
    [Authorize]
    public class ManufacturersController : Controller
    {
        
        private readonly IManufacturerRepository manufacturerRepository;

        public ManufacturersController(IManufacturerRepository manufacturerRepository)
        {
            this.manufacturerRepository = manufacturerRepository;
        }
        public IActionResult Index()
        {
            var Type = manufacturerRepository.GetManufacturers();
            return View(Type);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            ViewBag.Action = "edit";
            var manufacturer = manufacturerRepository.GetManufacturerById(id.HasValue ? id.Value : 0);
            return View(manufacturer);

        }

        [HttpPost]
        public IActionResult Edit(Manufacturer manufacturer)
        {
            if (ModelState.IsValid)
            {
                manufacturerRepository.Update(manufacturer.ManufacturerId, manufacturer);
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
                manufacturerRepository.Insert(manufacturer);
                return RedirectToAction(nameof(Index));
            }

            return View(manufacturer);
        }

        public IActionResult Delete(int id)
        {
            manufacturerRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
