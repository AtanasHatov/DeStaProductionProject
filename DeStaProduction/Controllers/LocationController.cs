using DeStaProduction.Infrastucture.Entities;
using DeStaProduction.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeStaProduction.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class LocationController : Controller
    {
        private readonly ApplicationDbContext context;

        public LocationController(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<IActionResult> Index()
        {
            var locations = await context.Locations
                .Select(x=>new LocationViewModel 
                { 
                    Id=x.Id,
                    Address=x.Address,
                    Capacity=x.Capacity,
                    City=x.City,
                    Name=x.Name,
                    Events=x.Events
                })
                .ToListAsync();
            return View(locations);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(LocationViewModel model)
        {

            var loc = new Location
            {
                Id = Guid.NewGuid(),
                Address = model.Address,
                Capacity = model.Capacity,
                City = model.City,
                Name = model.Name,
                Events = model.Events
            };

            await context.Locations.AddAsync(loc);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(Guid id)
        {
            var location = await context.Locations
                .Where(l => l.Id == id)
                .Select(x=>new LocationViewModel
                {
                    Id=x.Id,
                    Address = x.Address,
                    Capacity = x.Capacity,
                    City = x.City,
                    Name = x.Name,
                    Events = x.Events
                })
                .FirstOrDefaultAsync();

         

            return View(location);
        }
        public async Task<IActionResult> Delete(Guid id)
        {
            Location location = await context.Locations.FindAsync(id);

            if (location == null)
            {
                return NotFound();
            }
            LocationViewModel locationViewModel = new LocationViewModel
            {
                Address = location.Address,
                Capacity = location.Capacity,
                City = location.City,
                Name = location.Name,
                Events = location.Events
            };
            return View(locationViewModel);
        }
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var location = await context.Locations.FindAsync(id);

            if (location != null)
            {
                context.Locations.Remove(location);
                await context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
