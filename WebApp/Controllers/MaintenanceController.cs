using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Repositories;
using WebApp.Repositories.Interfaces;
using WebApp.ViewModels;


namespace WebApp.Controllers
{
    [Authorize]
    public class MaintenanceController : Controller
    {
        private readonly IMaintenanceRepository maintenanceRepository;
        private readonly IVehicleRepository vehicleRepository;

        public MaintenanceController(IMaintenanceRepository maintenanceRepository, IVehicleRepository vehicleRepository)
        {
            this.maintenanceRepository = maintenanceRepository;
            this.vehicleRepository = vehicleRepository;
        }
        public IActionResult Index(int? vehicleId)
        {
            if (vehicleId.HasValue)
            {
                var main = maintenanceRepository.GetMaintenanceByVehicleId(vehicleId.Value);
            }
            var maintainenance = maintenanceRepository.GetMaintenances();
            return View(maintainenance);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "edit";
            var maintenance = maintenanceRepository.GetMaintenanceById(id);
            if(maintenance != null)
            {
                var maintenanceViewModel = new MaintenanceViewModel
                {
                    Maintenance = maintenance,
                    Vehicles = vehicleRepository.GetVehicles()
                };
                return View(maintenanceViewModel);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Edit(MaintenanceViewModel maintenanceViewModel)
        {
            if (ModelState.IsValid)
            {
                maintenanceViewModel.Maintenance.Vehicle = vehicleRepository.GetVehicleById(maintenanceViewModel.Maintenance.VehicleId);
                maintenanceRepository.Update(maintenanceViewModel.Maintenance.MaintenanceId, maintenanceViewModel.Maintenance);
                return RedirectToAction(nameof(Index));
            }

            return View(maintenanceViewModel);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "add";
            var maintenance = new Maintenance
            {
                MaintenanceDate = DateTime.Now
            };
                var maintenanceViewModel = new MaintenanceViewModel
                {
                    Maintenance = maintenance,
                    Vehicles = vehicleRepository.GetVehicles()
                };
                return View(maintenanceViewModel);
        }
        [HttpPost]
        public IActionResult Add(MaintenanceViewModel maintenanceViewModel)
        {

            if (ModelState.IsValid)
            {
                
                maintenanceRepository.Insert(maintenanceViewModel.Maintenance);
                return RedirectToAction(nameof(Index));
            }

            return View(maintenanceViewModel);
        }

        public IActionResult Delete(int id)
        {
            maintenanceRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
