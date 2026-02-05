using DeStaProduction.Infrastucture.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DeStaProduction.Controllers
{
    //[Authorize(Roles = "Admin")]
    //„Event описва какво е събитието.Кога и къде се случва се описва в Performance, за да може един Event да има много изпълнения.“
    public class EventController : Controller
    {
        private readonly ApplicationDbContext context;

        public EventController(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<IActionResult> Index()
        {
            var events = await context.Events
                .Include(e => e.Type)
                .ToListAsync();

            return View(events);
        }
        public async Task<IActionResult> Create()
        {
            var types = await context.EventTypes.ToListAsync();
            ViewBag.EventTypes = new SelectList(types, "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Event model)
        {
            context.Events.Add(model);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(Guid id)
        {
            var ev = await context.Events
                .Include(e => e.Type)
                .Include(e => e.Performances)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (ev == null)
            {
                return NotFound();
            }

            return View(ev);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var ev = await context.Events.FirstOrDefaultAsync(a => a.Id == Id);
            if (ev == null)
            {
                return NotFound();
            }
            context.Events.Remove(ev);
            await context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
