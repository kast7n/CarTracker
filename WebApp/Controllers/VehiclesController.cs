using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class VehiclesController : Controller
    {
        public IActionResult Index()
        {
            var vehicles = VehiclesRepository.GetVehicles();
            return View(vehicles);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            ViewBag.Action = "edit";
            var vehicle = VehiclesRepository.GetVehicleById(id.HasValue ? id.Value : 0);
            return View(vehicle);
        }
        [HttpPost]
        public IActionResult Edit(Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                VehiclesRepository.UpdateVehicle(vehicle.VehicleId,vehicle); 
                return RedirectToAction(nameof(Index));
            }
            return View(vehicle);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "add";
            return View();
        }

        [HttpPost]
        public IActionResult Add(Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                VehiclesRepository.AddVehicle(vehicle);
                return RedirectToAction(nameof(Index));
            }
            return View(vehicle);
        }

        public IActionResult Delete(int vehicleId)
        {
            VehiclesRepository.DeleteVehicle(vehicleId);
            return RedirectToAction(nameof(Index));
        }



    }
}
