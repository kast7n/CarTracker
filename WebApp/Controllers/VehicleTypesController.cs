using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Repositories.Interfaces;


namespace WebApp.Controllers
{
    [Authorize]
    public class VehicleTypesController : Controller
    {
        private readonly IVehicleTypeRepository vehicleTypeRepository;

        public VehicleTypesController(IVehicleTypeRepository vehicleTypeRepository)
        {
            this.vehicleTypeRepository = vehicleTypeRepository;
        }

        public IActionResult Index()
        {
            var Type = vehicleTypeRepository.GetVehicleTypes();
            return View(Type);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            ViewBag.Action = "edit";
            var type = vehicleTypeRepository.GetVehicleTypeById(id.HasValue ? id.Value : 0);
            return View(type);

        }

        [HttpPost]
        public IActionResult Edit(VehicleType type)
        {
            if (ModelState.IsValid)
            {
                vehicleTypeRepository.Update(type.TypeId, type);
                return RedirectToAction(nameof(Index));
            }

            return View(type);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "add";
            return View();
        }
        [HttpPost]
        public IActionResult Add(VehicleType type)
        {

            if (ModelState.IsValid)
            {
                vehicleTypeRepository.Insert(type);
                return RedirectToAction(nameof(Index));
            }

            return View(type);
        }

        public IActionResult Delete(int id)
        {
            vehicleTypeRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
