﻿using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class DriversController : Controller
    {
        public IActionResult Index()
        {
            var drivers = DriversRepository.GetDrivers();
            return View(drivers);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            ViewBag.Action = "edit";
            var driver = DriversRepository.GetDriverById(id.HasValue? id.Value:0);
            return View(driver);

        }

        [HttpPost]
        public IActionResult Edit(Driver driver)
        {
            if (ModelState.IsValid)
            {
                DriversRepository.UpdateDriver(driver.DriverId, driver);
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
                DriversRepository.AddDriver(driver);
                return RedirectToAction(nameof(Index));
            }

            return View(driver);
        }

        public IActionResult Delete(int id)
        {
            DriversRepository.DeleteDriver(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
