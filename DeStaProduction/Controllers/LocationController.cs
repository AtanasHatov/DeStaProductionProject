using DeStaProduction.Infrastucture.Entities;
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
            var locations = await context.Locations.ToListAsync();
            return View(locations);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Location model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            context.Locations.Add(model);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(Guid id)
        {
            var location = await context.Locations
                .FirstOrDefaultAsync(l => l.Id == id);

            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }
        public async Task<IActionResult> Delete(Guid id)
        {
            var location = await context.Locations.FindAsync(id);

            if (location == null)
            {
                return NotFound();
            }

            return View(location);
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
