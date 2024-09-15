using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Repositories;
using WebApp.Repositories.Interfaces;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class VehicleDriversController : Controller
    {
        private readonly IVehicleDriverRepository vehicleDriverRepository;
        private readonly IDriverRepository driverRepository;
        private readonly IVehicleRepository vehicleRepository;

        public VehicleDriversController(IVehicleDriverRepository vehicleDriverRepository, IDriverRepository driverRepository, IVehicleRepository vehicleRepository)
        {
            this.vehicleDriverRepository = vehicleDriverRepository;
            this.driverRepository = driverRepository;
            this.vehicleRepository = vehicleRepository;
        }
 

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            ViewBag.Action = "edit";
            var vehicleDriverViewModel = new VehicleDriversViewModel
            {
                VehicleDriver = vehicleDriverRepository.GetVehicleDriver(id.HasValue ? id.Value : 0) ?? new VehicleDriver(),
                Drivers = driverRepository.GetDrivers()
            };
            return View(vehicleDriverViewModel);

        }

        [HttpPost]
        public IActionResult Edit(VehicleDriversViewModel vehicleDriverViewModel)
        {
            if (ModelState.IsValid)
            {
                vehicleDriverViewModel.VehicleDriver.Vehicle = vehicleRepository.GetVehicleById(vehicleDriverViewModel.VehicleDriver.VehicleId);
                vehicleDriverViewModel.VehicleDriver.Driver = driverRepository.GetDriverById(vehicleDriverViewModel.VehicleDriver.DriverId);
                vehicleDriverRepository.Update(vehicleDriverViewModel.VehicleDriver.VehicleDriverId, vehicleDriverViewModel.VehicleDriver);
                return RedirectToAction(nameof(Index), "History");
            }
            vehicleDriverViewModel.Drivers = driverRepository.GetDrivers();
            return View(vehicleDriverViewModel);
        }

        [HttpGet]
        public IActionResult Add(int id) // VehicleId passed so user doesn't select a vehicle in the form, vehicle is selected when he chooses which vehicle to add the vehicleDriver to
        {
            ViewBag.Action = "add";
            var vehicleDriver = new VehicleDriver
            {
                VehicleId = id,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now
            };

            var vehicleDriverViewModel = new VehicleDriversViewModel
            {
                VehicleDriver = vehicleDriver,
                Drivers = driverRepository.GetDrivers()
            };


            return View(vehicleDriverViewModel);
        }
        [HttpPost]
        public IActionResult Add(VehicleDriversViewModel vehicleDriverViewModel)
        {

            if (ModelState.IsValid)
            {
                vehicleDriverViewModel.VehicleDriver.Vehicle = vehicleRepository.GetVehicleById(vehicleDriverViewModel.VehicleDriver.VehicleId);
                vehicleDriverViewModel.VehicleDriver.Driver = driverRepository.GetDriverById(vehicleDriverViewModel.VehicleDriver.DriverId);
                
                vehicleDriverRepository.Insert(vehicleDriverViewModel.VehicleDriver);
                return RedirectToAction(nameof(Index), "History");
            }
           
                vehicleDriverViewModel.Drivers = driverRepository.GetDrivers();
                ViewBag.Action = "Add";
                return View(vehicleDriverViewModel);
            

            
        }

        public IActionResult Delete(int id)
        {
            vehicleDriverRepository.Delete(id);
            return RedirectToAction(nameof(Index), "History");
        }
    }
}
