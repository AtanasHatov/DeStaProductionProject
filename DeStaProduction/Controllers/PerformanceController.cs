using DeStaProduction.Core.Contracts;
using DeStaProduction.Core.DTOs;
using DeStaProduction.Infrastucture.Entities;
using DeStaProduction.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

[Authorize]
public class PerformanceController : Controller
{
    private readonly IPerformanceService performanceService;
    private readonly IEventService eventService;
    private readonly ILocationService locationService;

    public PerformanceController(IPerformanceService _performanceService,IEventService _eventService,ILocationService _locationService)
    {
        performanceService = _performanceService;
        eventService = _eventService;
        locationService = _locationService;
    }

    public async Task<IActionResult> Index()
    {
        var data = await performanceService.GetAllAsync();

        var model = data.Select(x => new PerformanceViewModel
        {
            Id = x.Id,
            Title = x.Title,
            Description = "",
            Event = x.Event,
            Location = x.Location,
            Date = x.Date,
            Participants = new List<DeStaUser>(), 
            Schedules = new List<Schedule>() 
        });

        return View(model);
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create()
    {
        var events = await eventService.GetAllAsync();
        var locations = await locationService.GetAllAsync();

        ViewBag.Events = events.Select(x => new SelectListItem
        {
            Value = x.Id.ToString(),
            Text = x.Title
        }).ToList();

        ViewBag.Locations = locations.Select(x => new SelectListItem
        {
            Value = x.Id.ToString(),
            Text = x.Name
        }).ToList();

        return View();
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(PerformanceViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var events = await eventService.GetAllAsync();
            var locations = await locationService.GetAllAsync();

            ViewBag.Events = events.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Title
            }).ToList();

            ViewBag.Locations = locations.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToList();

            return View(model);
        }

        await performanceService.AddAsync(new PerformanceDto
        {
            Title = model.Title,
            Description = model.Description, 
            Date = model.Date,
            EventId = model.EventId,
            LocationId = model.LocationId
        });

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Guid id)
    {
        await performanceService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
