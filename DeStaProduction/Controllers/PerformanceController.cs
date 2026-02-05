using DeStaProduction.Infrastucture.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DeStaProduction.Controllers
{
    public class PerformanceController : Controller
    {
        private readonly ApplicationDbContext context;

        public PerformanceController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            var performances = await context.Performances
                .Include(p => p.Event)
                .Include(p => p.Location)
                .ToListAsync();

            return View(performances);
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Events = new SelectList(
                await context.Events.ToListAsync(),
                "Id",
                "Title"
            );

            ViewBag.Locations = new SelectList(
                await context.Locations.ToListAsync(),
                "Id",
                "Name"
            );

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Performance model)
        {
            context.Performances.Add(model);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var performance = await context.Performances.FirstOrDefaultAsync(p => p.Id == id);

            if (performance == null)
            {
                return NotFound();
            }

            context.Performances.Remove(performance);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}
