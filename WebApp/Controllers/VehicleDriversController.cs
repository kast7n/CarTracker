using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class VehicleDriversController : Controller
    {
        public IActionResult Index()
        {
            var maintainenance = VehicleDriversRepository.GetVehicleDrivers();
            return View(maintainenance);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            ViewBag.Action = "edit";
            var vehicleDriverViewModel = new VehicleDriversViewModel
            {
                VehicleDriver = VehicleDriversRepository.GetVehicleDriver(id.HasValue ? id.Value : 0) ?? new VehicleDriver(),
                drivers = DriversRepository.GetDrivers()
            };
            return View(vehicleDriverViewModel);

        }

        [HttpPost]
        public IActionResult Edit(VehicleDriversViewModel vehicleDriverViewModel)
        {
            if (ModelState.IsValid)
            {
                VehicleDriversRepository.UpdateVehicleDriver(vehicleDriverViewModel.VehicleDriver.AssignmentId, vehicleDriverViewModel.VehicleDriver);
                return RedirectToAction(nameof(Index), "History");
            }

            return View(vehicleDriverViewModel);
        }

        [HttpGet]
        public IActionResult Add(int id) // VehicleId passed so user doesn't select a vehicle in the form, vehicle is selected when he chooses which vehicle to add the vehicleDriver to
        {
            ViewBag.Action = "add";
            var vehicleDriver = new VehicleDriver
            {
                VehicleId = id
            };

            var vehicleDriverViewModel = new VehicleDriversViewModel
            {
                VehicleDriver = vehicleDriver,
                drivers = DriversRepository.GetDrivers()
            };


            return View(vehicleDriverViewModel);
        }
        [HttpPost]
        public IActionResult Add(VehicleDriversViewModel vehicleDriverViewModel)
        {

            if (ModelState.IsValid)
            {
                vehicleDriverViewModel.VehicleDriver.Vehicle = VehiclesRepository.GetVehicleById(vehicleDriverViewModel.VehicleDriver.VehicleId);
                VehicleDriversRepository.AddVehicleDriver(vehicleDriverViewModel.VehicleDriver);
                return RedirectToAction(nameof(Index), "History");
            }

            return View(vehicleDriverViewModel);
        }

        public IActionResult Delete(int id)
        {
            VehicleDriversRepository.DeleteVehicleDriver(id);
            return RedirectToAction(nameof(Index), "History");
        }
    }
}
