using DeStaProduction.Core.Contracts;
using DeStaProduction.Core.DTOs;
using DeStaProduction.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
public class PerformanceController : Controller
{
    private readonly IPerformanceService performanceService;

    public PerformanceController(IPerformanceService _performanceService)
    {
        performanceService = _performanceService;
    }

    public async Task<IActionResult> Index()
    {
        var data = await performanceService.GetAllAsync();

        var model = data.Select(x => new PerformanceViewModel
        {
            Id = x.Id,
            Title = x.Title,
            Event = x.Event,
            Location = x.Location,
            Date = x.Date
        });

        return View(model);
    }

    [Authorize(Roles = "Admin")]
    public IActionResult Create() => View();

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(PerformanceViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        await performanceService.AddAsync(new PerformanceDto
        {
            Title = model.Title,
            Date = model.Date
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
