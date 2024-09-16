using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Repositories.Interfaces;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    [Authorize(Policy = "Manager")]
    public class VehiclesController : Controller
    {
        private readonly IVehicleRepository vehicleRepository;
        private readonly IManufacturerRepository manufacturerRepository;
        private readonly IVehicleTypeRepository vehicleTypeRepository;

        public VehiclesController(IVehicleRepository vehicleRepository, IManufacturerRepository manufacturerRepository, IVehicleTypeRepository vehicleTypeRepository)
        {
            this.vehicleRepository = vehicleRepository;
            this.manufacturerRepository = manufacturerRepository;
            this.vehicleTypeRepository = vehicleTypeRepository;
        }
        public IActionResult Index()
        {
            var vehicles = vehicleRepository.GetVehicles(loadInfo: true);
            return View(vehicles);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "edit";

            var vehicleViewModel = new VehiclesViewModel
            {
                Vehicle = vehicleRepository.GetVehicleById(id, loadInfo: true) ??new Vehicle(),
                Manufacturers = manufacturerRepository.GetManufacturers(),
                VehicleTypes = vehicleTypeRepository.GetVehicleTypes()
            };
            
            return View(vehicleViewModel);
        }
        [HttpPost]
        public IActionResult Edit(VehiclesViewModel vehicleViewModel)
        {
            if (ModelState.IsValid)
            {
                vehicleRepository.Update(vehicleViewModel.Vehicle.VehicleId,vehicleViewModel.Vehicle); 
                return RedirectToAction(nameof(Index));
            }
            return View(vehicleViewModel);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "add";
            var vehicleViewmodel = new VehiclesViewModel
            {
                Manufacturers = manufacturerRepository.GetManufacturers(),
                VehicleTypes = vehicleTypeRepository.GetVehicleTypes()
            };
            return View(vehicleViewmodel);
        }

        [HttpPost]
        public IActionResult Add(VehiclesViewModel vehicleViewModel)
        {
            if (ModelState.IsValid)
            {
                vehicleRepository.Insert(vehicleViewModel.Vehicle);
                return RedirectToAction(nameof(Index));
            }
            return View(vehicleViewModel);
        }

        public IActionResult Delete(int vehicleId)
        {
            vehicleRepository.Delete(vehicleId);
            return RedirectToAction(nameof(Index));
        }



    }
}
