using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class VehicleTypesController : Controller
    {
        public IActionResult Index()
        {
            var Type = VehicleTypeRepository.GetVehicleTypes();
            return View(Type);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            ViewBag.Action = "edit";
            var type = VehicleTypeRepository.GetVehicleTypeById(id.HasValue ? id.Value : 0);
            return View(type);

        }

        [HttpPost]
        public IActionResult Edit(VehicleType type)
        {
            if (ModelState.IsValid)
            {
                VehicleTypeRepository.UpdateVehicleType(type.TypeId, type);
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
                VehicleTypeRepository.AddVehicleType(type);
                return RedirectToAction(nameof(Index));
            }

            return View(type);
        }

        public IActionResult Delete(int id)
        {
            VehicleTypeRepository.DeleteVehicleType(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
