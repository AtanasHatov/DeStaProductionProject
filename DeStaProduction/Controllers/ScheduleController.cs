using DeStaProduction.Core.Contracts;
using DeStaProduction.Infrastucture.Entities;
using DeStaProduction.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[Authorize]
public class ScheduleController : Controller
{
    private readonly IScheduleService scheduleService;
    private readonly UserManager<DeStaUser> userManager;

    public ScheduleController(IScheduleService scheduleService, UserManager<DeStaUser> userManager)
    {
        this.scheduleService = scheduleService;
        this.userManager = userManager;
    }

    public async Task<IActionResult> Index(int? month, int? year)
    {
        int m = month ?? DateTime.Now.Month;
        int y = year ?? DateTime.Now.Year;

        ViewBag.Month = m;
        ViewBag.Year = y;

        var user = await userManager.GetUserAsync(User);

        var role = User.IsInRole("Admin") ? "Admin" :
                   User.IsInRole("Artist") ? "Artist" : "User";

        var data = await scheduleService.GetAllAsync(m, y, role, user.Id);


        var model = data.Select(x => new ScheduleViewModel
        {
            Id = x.Id,
            Date = x.Date,
            IsAvailable = x.IsAvailable,
            Notes = x.Notes ?? "",
            UserId = x.UserId,
            Type = x.Type,
            FirstName = x.FirstName,
            LastName = x.LastName,
            UserName = x.UserName,
            LocationName = x.LocationName,
            PerformanceTitle = x.PerformanceTitle    
        });

        return View(model);
    }
}