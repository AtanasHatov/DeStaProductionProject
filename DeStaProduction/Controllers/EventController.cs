using DeStaProduction.Core.Contracts;
using DeStaProduction.Core.DTOs;
using DeStaProduction.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

[Authorize(Roles = "Admin")]
public class EventController : Controller
{
    private readonly IEventService eventService;
    private readonly IEventTypeService eventTypeService;

    public EventController(IEventService _eventService, IEventTypeService _eventTypeService)
    {
        eventService = _eventService;
        eventTypeService = _eventTypeService;
    }

    public async Task<IActionResult> Index()
    {
        var data = await eventService.GetAllAsync();

        var model = data.Select(e => new EventViewModel
        {
            Id = e.Id,
            Title = e.Title,
            Description = e.Description,
            Duration = e.Duration,
            TypeName = e.TypeName
        });

        return View(model);
    }

    public async Task<IActionResult> Create()
    {
        var types = await eventTypeService.GetAllAsync();

        ViewBag.EventTypes = types
            .Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToList();

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(EventViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        if (!ModelState.IsValid)
        {
            var types = await eventTypeService.GetAllAsync();

            ViewBag.EventTypes = types
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                }).ToList();

            return View(model);
        }

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

    public async Task<IActionResult> Details(Guid id)
{
        var item = await eventService.GetByIdAsync(id);

        if (item == null)
            return NotFound();

        var model = new EventViewModel
        {
            Id = item.Id,
            Title = item.Title,
            Description = item.Description,
            Duration = item.Duration,
            TypeName = item.TypeName,
            Performances = item.Performances
        };

        return View(model);
    }
}