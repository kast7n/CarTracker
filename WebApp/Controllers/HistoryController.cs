using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Repositories;
using WebApp.Repositories.Interfaces;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    [Authorize]
    public class HistoryController : Controller
    {
        private readonly IVehicleRepository vehicleRepository;
        private readonly IVehicleTypeRepository vehicleTypeRepository;
        private readonly IMaintenanceRepository maintenanceRepository;
        private readonly IVehicleDriverRepository vehicleDriverRepository;
        private readonly IManufacturerRepository manufacturerRepository;

        public HistoryController(IVehicleRepository vehicleRepository,IVehicleTypeRepository vehicleTypeRepository, IMaintenanceRepository maintenanceRepository, IVehicleDriverRepository vehicleDriverRepository,IManufacturerRepository manufacturerRepository)
        {
            this.vehicleRepository = vehicleRepository;
            this.vehicleTypeRepository = vehicleTypeRepository;
            this.maintenanceRepository = maintenanceRepository;
            this.vehicleDriverRepository = vehicleDriverRepository;
            this.manufacturerRepository = manufacturerRepository;
        }
        public IActionResult Index(int? typeId, int? manufacturerId)
        {
            var historyViewModel = new HistoryViewModel
            {
                Vehicles = vehicleRepository.GetVehicles(),
                VehicleTypes = vehicleTypeRepository.GetVehicleTypes(),
                Manufacturers = manufacturerRepository.GetManufacturers()
            };

            if (typeId.HasValue)
            {
                historyViewModel.Vehicles = historyViewModel.Vehicles.Where(v => v.TypeId == typeId.Value);
                historyViewModel.SelectedTypeId = typeId;
            }

            if (manufacturerId.HasValue)
            {
                historyViewModel.Vehicles = historyViewModel.Vehicles.Where(v => v.ManufacturerId == manufacturerId.Value);
                historyViewModel.SelectedManufacturerId = manufacturerId;
            }
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
