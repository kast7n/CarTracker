using Microsoft.AspNetCore.Mvc;
using WebApp.Repositories.Interfaces;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class HistoryController : Controller
    {
        private readonly IVehicleRepository vehicleRepository;
        private readonly IMaintenanceRepository maintenanceRepository;
        private readonly IVehicleDriverRepository vehicleDriverRepository;

        public HistoryController(IVehicleRepository vehicleRepository, IMaintenanceRepository maintenanceRepository, IVehicleDriverRepository vehicleDriverRepository)
        {
            this.vehicleRepository = vehicleRepository;
            this.maintenanceRepository = maintenanceRepository;
            this.vehicleDriverRepository = vehicleDriverRepository;
        }
        public IActionResult Index()
        {
            var historyViewModel = new HistoryViewModel
            {
                Vehicles = vehicleRepository.GetVehicles()
            };
            return View(historyViewModel);
        }

        public IActionResult MaintenanceHistoryByVehiclePartial(int vehicleId)
        {
            var history = maintenanceRepository.GetMaintenanceByVehicleId(vehicleId);

            return PartialView("_MaintenanceHistory", history);
        }

        public IActionResult DriversHistoryByVehiclePartial(int vehicleId)
        {
            var history = vehicleDriverRepository.GetVehicleDriversByVehicleId(vehicleId,loadInfo: true);

            return PartialView("_DriversHistory", history);
        }

    }
}
