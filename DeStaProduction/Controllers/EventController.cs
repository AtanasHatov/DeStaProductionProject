using DeStaProduction.Infrastucture.Entities;
using DeStaProduction.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace DeStaProduction.Controllers
{
    //„Event описва какво е събитието.Кога и къде се случва се описва в Performance, за да може един Event да има много изпълнения.“
    using DeStaProduction.Core.Contracts;
    using DeStaProduction.Core.DTOs;
    using DeStaProduction.ViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = "Admin")]
    public class EventController : Controller
    {
        private readonly IEventService eventService;

        public EventController(IEventService _eventService)
        {
            eventService = _eventService;
        }

        public async Task<IActionResult> Index()
        {
            var events = await eventService.GetAllAsync();

            var model = events.Select(e => new EventViewModel
            {
                Id = e.Id,
                Title = e.Title,
                Description = e.Description,
                Duration = e.Duration,
                TypeName = e.TypeName
            });

            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EventViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await eventService.AddAsync(new AddEventDto
            {
                Title = model.Title,
                Description = model.Description,
                Duration = model.Duration,
                EventTypeId = model.EventType
            });

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            await eventService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
