using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Repositories;
using WebApp.Repositories.Interfaces;


namespace WebApp.Controllers
{
    [Authorize]
    public class DriversController : Controller
    {
        private readonly IDriverRepository driverRepository;
        private readonly IVehicleDriverRepository vehicleDriverRepository;

        public DriversController(IDriverRepository driverRepository, IVehicleDriverRepository vehicleDriverRepository)
        {
            this.driverRepository = driverRepository;
            this.vehicleDriverRepository = vehicleDriverRepository;
        }
        public IActionResult Index()
        {
            var drivers = driverRepository.GetDrivers();
            return View(drivers);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            ViewBag.Action = "edit";
            var driver = driverRepository.GetDriverById(id.HasValue? id.Value:0);
            return View(driver);

        }

        [HttpPost]
        public IActionResult Edit(Driver driver)
        {
            if (ModelState.IsValid)
            {
                driverRepository.Update(driver.DriverId, driver);
                return RedirectToAction(nameof(Index));
            }

            return View(driver);
        }

        [HttpGet]
        public IActionResult Add() // VehicleId passed so user doesn't select a vehicle in the form, vehicle is selected when he chooses which vehicle to add the driver to
        {
            ViewBag.Action = "add";    
            return View();
        }
        [HttpPost]
        public IActionResult Add(Driver driver)
        {
            
            if (ModelState.IsValid)
            {
                driverRepository.Insert(driver);
                return RedirectToAction(nameof(Index));
            }

            return View(driver);
        }

        public IActionResult Delete(int id)
        {
            driverRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult DriverDetails(int id)//driverId
        {
            var driver = driverRepository.GetDriverById(id);
            if (driver != null)
                driver.VehicleDrivers = vehicleDriverRepository.GetVehicleDriversByDriverId(id).ToList();
            return View(driver);
        }
    }
}
