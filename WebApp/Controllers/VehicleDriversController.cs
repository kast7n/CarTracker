using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Repositories;
using WebApp.Repositories.Interfaces;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    [Authorize]
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

        public IActionResult Index(int? vehicleId)
        {
            if(vehicleId.HasValue)
            {
                List<VehicleDriver> vd = vehicleDriverRepository.GetVehicleDriversByVehicleId(vehicleId.Value,true).ToList();
                return View(vd);
            }

            var vehicleDrivers = vehicleDriverRepository.GetVehicleDrivers(true);
            return View(vehicleDrivers);
        }
 

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            ViewBag.Action = "edit";
            var vehicleDriverViewModel = new VehicleDriversViewModel
            {
                VehicleDriver = vehicleDriverRepository.GetVehicleDriver(id.HasValue ? id.Value : 0) ?? new VehicleDriver(),
                Drivers = driverRepository.GetDrivers(),
                Vehicles = vehicleRepository.GetVehicles()
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
                return RedirectToAction(nameof(Index));
            }
            vehicleDriverViewModel.Drivers = driverRepository.GetDrivers();
            vehicleDriverViewModel.Vehicles = vehicleRepository.GetVehicles();
            return View(vehicleDriverViewModel);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "add";
            var vehicleDriver = new VehicleDriver
            {
        
                StartDate = DateTime.Now,
                EndDate = DateTime.Now
            };

            var vehicleDriverViewModel = new VehicleDriversViewModel
            {
                VehicleDriver = vehicleDriver,
                Drivers = driverRepository.GetDrivers(),
                Vehicles = vehicleRepository.GetVehicles()
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
                return RedirectToAction(nameof(Index));
            }
           
                vehicleDriverViewModel.Drivers = driverRepository.GetDrivers();
                vehicleDriverViewModel.Vehicles = vehicleRepository.GetVehicles();
                ViewBag.Action = "Add";
                return View(vehicleDriverViewModel);
            

            
        }

        public IActionResult Delete(int id)
        {
            vehicleDriverRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
