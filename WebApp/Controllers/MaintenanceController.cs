using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Repositories.Interfaces;


namespace WebApp.Controllers
{
    public class MaintenanceController : Controller
    {
        private readonly IMaintenanceRepository maintenanceRepository;
        private readonly IVehicleRepository vehicleRepository;

        public MaintenanceController(IMaintenanceRepository maintenanceRepository, IVehicleRepository vehicleRepository)
        {
            this.maintenanceRepository = maintenanceRepository;
            this.vehicleRepository = vehicleRepository;
        }
        public IActionResult Index()
        {
            var maintainenance = maintenanceRepository.GetMaintenanceHistory();
            return View(maintainenance);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            ViewBag.Action = "edit";
            var maintenance = maintenanceRepository.GetMaintenanceById(id.HasValue ? id.Value : 0);
            return View(maintenance);

        }

        [HttpPost]
        public IActionResult Edit(Maintenance maintenance)
        {
            if (ModelState.IsValid)
            {
                maintenanceRepository.Update(maintenance.MaintenanceId, maintenance);
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
                maintenance.Vehicle = vehicleRepository.GetVehicleById(maintenance.VehicleId);
                maintenanceRepository.Insert(maintenance);
                return RedirectToAction(nameof(Index), "History");
            }

            return View(maintenance);
        }

        public IActionResult Delete(int id)
        {
            maintenanceRepository.Delete(id);
            return RedirectToAction(nameof(Index), "History");
        }
    }
}
