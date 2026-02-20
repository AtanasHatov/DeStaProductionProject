using DeStaProduction.Infrastucture.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace DeStaProduction.Controllers
{
    [Authorize]
    public class ScheduleController : Controller
    {
            private readonly ApplicationDbContext context;
            private readonly UserManager<DeStaUser> userManager;

            public ScheduleController(ApplicationDbContext context, UserManager<DeStaUser> userManager)
            {
                this.context = context;
                this.userManager = userManager;
            }

        [Authorize(Roles = "Admin,Artist")]
        public async Task<IActionResult> Index()
        {
            var schedules = await context.Schedules
                .Include(s => s.User)          
                .Include(s => s.Performance)   
                .ThenInclude(p => p.Event)
                .ToListAsync();

            return View(schedules);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Performances = new SelectList(
                await context.Performances.ToListAsync(),
                "Id",
                "Title");

            ViewBag.Artists = new SelectList(
                await userManager.GetUsersInRoleAsync("Artist"),
                "Id",
                "UserName");

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(Schedule model)
        {
            var performance = await context.Performances
                            .Include(p => p.Event)
                            .FirstOrDefaultAsync(p => p.Id == model.PerformanceId);

            if (performance == null)
            {
                ModelState.AddModelError("", "Invalid performance.");
                return View(model);
            }

            var start = performance.Date;
            var end = performance.Date.AddMinutes(performance.Event.Duration);

            var hasConflict = await context.Schedules
                .Include(s => s.Performance)
                .ThenInclude(p => p.Event)
                .AnyAsync(s =>
                    s.UserId == model.UserId &&
                    s.Performance.Date < end &&
                    s.Performance.Date.AddMinutes(s.Performance.Event.Duration) > start
                );

            if (hasConflict)
            {
                ModelState.AddModelError("", "The artist is already busy at this time.");
                return View(model);
            }

            context.Schedules.Add(model);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var schedule = await context.Schedules.FindAsync(id);
            if (schedule == null)
                return NotFound();

            return View(schedule);
        }

        [Authorize(Roles = "Artist")]
        public async Task<IActionResult> Availability(Guid id)
        {
            var user = await userManager.GetUserAsync(User);

            var schedule = await context.Schedules
                .FirstOrDefaultAsync(s => s.Id == id && s.UserId == user.Id);

            if (schedule == null)
                return Unauthorized();

            return View(schedule);
        }

        [HttpPost]
        [Authorize(Roles = "Artist")]
        public async Task<IActionResult> Availability(Guid id, bool isAvailable, string notes)
        {
            var user = await userManager.GetUserAsync(User);

            var schedule = await context.Schedules
                .FirstOrDefaultAsync(s => s.Id == id && s.UserId == user.Id);

            if (schedule == null)
                return Unauthorized();

            schedule.IsAvailable = isAvailable;
            schedule.Notes = notes;

            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var schedule = await context.Schedules.FindAsync(id);
            if (schedule == null)
                return NotFound();

            context.Schedules.Remove(schedule);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
