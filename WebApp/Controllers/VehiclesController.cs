using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class VehiclesController : Controller
    {
        public IActionResult Index()
        {
            var vehicles = VehiclesRepository.GetVehicles(loadInfo: true);
            return View(vehicles);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "edit";

            var vehicleViewModel = new VehiclesViewModel
            {
                Vehicle = VehiclesRepository.GetVehicleById(id, loadInfo: true) ??new Vehicle(),
                Manufacturers = VehicleManufacturerRepository.GetManufacturers(),
                VehicleTypes = VehicleTypeRepository.GetVehicleTypes()
            };
            
            return View(vehicleViewModel);
        }
        [HttpPost]
        public IActionResult Edit(VehiclesViewModel vehicleViewModel)
        {
            if (ModelState.IsValid)
            {
                VehiclesRepository.UpdateVehicle(vehicleViewModel.Vehicle.VehicleId,vehicleViewModel.Vehicle); 
                return RedirectToAction(nameof(Index));
            }
            return View(vehicleViewModel);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "add";
            var vehicleViewmodel = new VehiclesViewModel
            {
                Manufacturers = VehicleManufacturerRepository.GetManufacturers(),
                VehicleTypes = VehicleTypeRepository.GetVehicleTypes()
            };
            return View(vehicleViewmodel);
        }

        [HttpPost]
        public IActionResult Add(VehiclesViewModel vehicleViewModel)
        {
            if (ModelState.IsValid)
            {
                VehiclesRepository.AddVehicle(vehicleViewModel.Vehicle);
                return RedirectToAction(nameof(Index));
            }
            return View(vehicleViewModel);
        }

        public IActionResult Delete(int vehicleId)
        {
            VehiclesRepository.DeleteVehicle(vehicleId);
            return RedirectToAction(nameof(Index));
        }



    }
}
