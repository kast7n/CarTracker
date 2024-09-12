using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class MaintenanceController : Controller
    {
        public IActionResult Index()
        {
            var maintainenance = MaintenanceRepository.GetMaintenanceHistory();
            return View(maintainenance);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            ViewBag.Action = "edit";
            var maintenance = MaintenanceRepository.GetMaintenanceHistoryById(id.HasValue ? id.Value : 0);
            return View(maintenance);

        }

        [HttpPost]
        public IActionResult Edit(Maintenance maintenance)
        {
            if (ModelState.IsValid)
            {
                MaintenanceRepository.UpdateMaintenanceHistory(maintenance.MaintenanceId, maintenance);
                return RedirectToAction(nameof(Index), "History");
            }

            return View(maintenance);
        }

        [HttpGet]
        public IActionResult Add(int id) // VehicleId passed so user doesn't select a vehicle in the form, vehicle is selected when he chooses which vehicle to add the maintenance to
        {
            ViewBag.Action = "add";
            var maintenance = new Maintenance
            {
                VehicleId = id
            };
            return View(maintenance);
        }
        [HttpPost]
        public IActionResult Add(Maintenance maintenance)
        {

            if (ModelState.IsValid)
            {
                maintenance.Vehicle = VehiclesRepository.GetVehicleById(maintenance.VehicleId);
                MaintenanceRepository.AddMaintenanceHistory(maintenance);
                return RedirectToAction(nameof(Index), "History");
            }

            return View(maintenance);
        }

        public IActionResult Delete(int id)
        {
            MaintenanceRepository.DeleteMaintenanceHistory(id);
            return RedirectToAction(nameof(Index), "History");
        }
    }
}
