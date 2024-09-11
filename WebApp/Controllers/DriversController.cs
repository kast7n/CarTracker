using Microsoft.AspNetCore.Mvc;
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
                return RedirectToAction(nameof(Index),"History");
            }

            return View(driver);
        }
    }
}
