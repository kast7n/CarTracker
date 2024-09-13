using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Repositories;
using WebApp.Repositories.Interfaces;


namespace WebApp.Controllers
{
    public class DriversController : Controller
    {
        private readonly IDriverRepository _driverRepository;
        public DriversController(IDriverRepository driverRepository)
        {
            _driverRepository = driverRepository;
        }
        public IActionResult Index()
        {
            var drivers = _driverRepository.GetDrivers();
            return View(drivers);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            ViewBag.Action = "edit";
            var driver = _driverRepository.GetDriverById(id.HasValue? id.Value:0);
            return View(driver);

        }

        [HttpPost]
        public IActionResult Edit(Driver driver)
        {
            if (ModelState.IsValid)
            {
                _driverRepository.Update(driver.DriverId, driver);
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
                _driverRepository.Insert(driver);
                return RedirectToAction(nameof(Index));
            }

            return View(driver);
        }

        public IActionResult Delete(int id)
        {
            _driverRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
