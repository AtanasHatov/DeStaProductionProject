using DeStaProduction.Infrastucture.Entities;
using DeStaProduction.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace DeStaProduction.Controllers
{
    using DeStaProduction.Core.Contracts;
    using DeStaProduction.ViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = "Admin")]
    public class EventTypeController : Controller
    {
        private readonly IEventTypeService eventTypeService;

        public EventTypeController(IEventTypeService _eventTypeService)
        {
            eventTypeService = _eventTypeService;
        }

        public async Task<IActionResult> Index()
        {
            var data = await eventTypeService.GetAllAsync();

            var model = data.Select(x => new EventTypeViewModel
            {
                Id = x.Id,
                Name = x.Name
            });

            return View(model);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(EventTypeViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await eventTypeService.AddAsync(model.Name);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            await eventTypeService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
