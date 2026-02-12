using DeStaProduction.Infrastucture.Entities;
using DeStaProduction.ViewModels;
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
            var eventTypes = await context.EventTypes
                .Select(x=> new EventTypeViewModel
                {
                    Id=x.Id,
                    Name=x.Name,
                    Events=x.Events

                })
                .ToListAsync();
            return View(eventTypes);
        }

 
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EventTypeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var evtp = new EventType 
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Events = model.Events
            };


            await context.EventTypes.AddAsync(evtp);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(Guid id)
        {
            EventType eventType = await context.EventTypes.FindAsync(id);

            if (eventType == null)
            {
                return NotFound();
            }
            EventTypeViewModel eventTypeViewModel = new EventTypeViewModel
            {
                Id = eventType.Id,
                Name = eventType.Name,
                Events = eventType.Events
            };
            return View(eventTypeViewModel);
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
