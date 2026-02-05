using DeStaProduction.Infrastucture.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeStaProduction.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class EventTypeController : Controller
    {
        private readonly ApplicationDbContext context;

        public EventTypeController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            var eventTypes = await context.EventTypes.ToListAsync();
            return View(eventTypes);
        }

 
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EventType model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            context.EventTypes.Add(model);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(Guid id)
        {
            var eventType = await context.EventTypes.FindAsync(id);

            if (eventType == null)
            {
                return NotFound();
            }

            return View(eventType);
        }
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var eventType = await context.EventTypes.FindAsync(id);

            if (eventType != null)
            {
                context.EventTypes.Remove(eventType);
                await context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
