using DeStaProduction.Core.Contracts;
using DeStaProduction.Core.DTOs;
using DeStaProduction.Core.Services;
using DeStaProduction.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;

[Authorize(Roles = "Admin")]
public class EventController : Controller
{
    private readonly IEventService eventService;
    private readonly IEventTypeService eventTypeService;
    private readonly IImageService imageService;

    public EventController(IEventService eventService,
                       IEventTypeService eventTypeService,
                       IImageService imageService)
    {
        this.eventService = eventService;
        this.eventTypeService = eventTypeService;
        this.imageService = imageService;
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
            TypeName = e.TypeName,
            ImagePath=e.ImagePath
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
        string imageUrl = null;

        if (model.ImageFile != null)
        {
            imageUrl = await imageService.UploadImageAsync(
                model.ImageFile,
                model.ImageFile.FileName);
        }

        await eventService.AddAsync(new AddEventDto
        {
            Title = model.Title,
            Description = model.Description,
            Duration = model.Duration,
            EventTypeId = model.EventType,
            ImagePath = imageUrl
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
            Performances = item.Performances,
            ImagePath = item.ImagePath
        };

        return View(model);
    }
}