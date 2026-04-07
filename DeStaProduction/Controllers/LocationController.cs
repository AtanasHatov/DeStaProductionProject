using DeStaProduction.Core.Contracts;
using DeStaProduction.Core.DTOs;
using DeStaProduction.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeStaProduction.Controllers
{
  

    [Authorize(Roles = "Admin")]
    public class LocationController : Controller
    {
        private readonly ILocationService locationService;

        public LocationController(ILocationService _locationService)
        {
            locationService = _locationService;
        }

        public async Task<IActionResult> Index()
        {
            var data = await locationService.GetAllAsync();

            var model = data.Select(x => new LocationViewModel
            {
                Id = x.Id,
                Name = x.Name,
                City = x.City,
                Capacity=x.Capacity
            });

            return View(model);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(LocationViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await locationService.AddAsync(new LocationDto
            {
                Name = model.Name,
                City = model.City,
                Capacity = model.Capacity,
                Address = model.Address
            });

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmd(Guid id)
        {
            await locationService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var data = await locationService.GetAllAsync();
            var item = data.FirstOrDefault(x => x.Id == id);

            if (item == null)
                return NotFound();

            var model = new LocationViewModel
            {
                Id = item.Id,
                Name = item.Name,
                City = item.City,
                Capacity = item.Capacity
            };

            return View(model);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var data = await locationService.GetAllAsync();
            var item = data.FirstOrDefault(x => x.Id == id);

            if (item == null)
                return NotFound();

            var model = new LocationViewModel
            {
                Id = item.Id,
                Name = item.Name,
                City = item.City,
                Capacity = item.Capacity,
                Address = item.Address
            };

            return View(model);
        }
    }
}
