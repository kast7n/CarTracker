using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class HistoryController : Controller
    {
        public IActionResult Index()
        {
            var historyViewModel = new HistoryViewModel
            {
                Vehicles = VehiclesRepository.GetVehicles()
            };
            return View(historyViewModel);
        }

        public IActionResult MaintenanceHistoryByVehiclePartial(int vehicleId)
        {
            var history = MaintenanceRepository.GetMaintenanceByVehicleId(vehicleId);

            return PartialView("_MaintenanceHistory", history);
        }

        public IActionResult DriversHistoryByVehiclePartial(int vehicleId)
        {
            var history = VehicleDriverRepository.GetVehicleDriversByVehicleId(vehicleId);

            return PartialView("_DriversHistory", history);
        }

    }
}
