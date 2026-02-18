using DeStaProduction.Infrastucture.Entities;
using DeStaProduction.ViewModels;
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
                .Include(e => e.Type).Select(x=> new EventViewModel
                {
                    EventType = x.EventType,
                    Description = x.Description,
                    Duration = x.Duration,
                    Id=x.Id,
                    Performances = x.Performances,
                    Title = x.Title,
                    Type = x.Type,
                    Users = x.Users
                })
                .ToListAsync();

            return View(events);
        }
        public async Task<IActionResult> Create()
        {
            var types = await context.EventTypes.ToListAsync();
            ViewBag.EventTypes = new SelectList(types, "Id", "Name");
            return View(new EventViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Create(EventViewModel model)
        {

            var events = new Event 
            {
                EventType = model.EventType,
                Description = model.Description,
                Duration = model.Duration,
                Id = Guid.NewGuid(),
                Performances = model.Performances,
                Title = model.Title,
                Type = model.Type,
                Users = model.Users
            };

            await  context.Events.AddAsync(events);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(Guid id)
        {
            var ev = await context.Events
                .Include(e => e.Type)
                .Include(e => e.Performances)
                .Where(e => e.Id == id)
                .Select(x => new EventViewModel 
                {
                    EventType = x.EventType,
                    Description = x.Description,
                    Duration = x.Duration,
                    Id = x.Id,
                    Performances = x.Performances,
                    Title = x.Title,
                    Type = x.Type,
                    Users = x.Users
                })
                .FirstOrDefaultAsync();


            if (ev == null)
            {
                return NotFound();
            }

            return View(ev);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var ev = await context.Events.Where(a => a.Id == Id)
                .FirstOrDefaultAsync();

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
