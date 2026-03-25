using DeStaProduction.Infrastucture.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DeStaProduction.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<DeStaUser> userManager;
        private readonly ApplicationDbContext context;

        public AdminController(UserManager<DeStaUser> userManager, ApplicationDbContext context)
        {
            this.userManager = userManager;
            this.context = context;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var users = userManager.Users.ToList();
            return View(users);
        }

        public IActionResult PendingUsers()
        {
            var users = userManager.Users
                .Where(u => !u.IsApproved)
                .ToList();

            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> Approve(Guid userId, string role)
        {
            var user = await userManager.FindByIdAsync(userId.ToString());

            if (user == null)
                return NotFound();

            user.IsApproved = true;
            await userManager.UpdateAsync(user);

            await userManager.AddToRoleAsync(user, role);

            return RedirectToAction(nameof(PendingUsers));
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AddPerformance()
        {
            ViewBag.Performances = new SelectList(context.Performances, "Id", "Title");
            ViewBag.Actors = new SelectList(context.Users, "Id", "UserName");

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddPerformance(Guid performanceId, List<Guid> actorIds)
        {
            var performance = await context.Performances.FindAsync(performanceId);

            foreach (var actorId in actorIds)
            {
                context.Schedules.Add(new Schedule
                {
                    UserId = actorId,
                    PerformanceId = performanceId,
                    Date = performance.Date,
                    Type = "Performance",
                    IsPublic = true
                });
            }

            await context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AssignTask()
        {
            ViewBag.Actors = new SelectList(context.Users, "Id", "UserName");
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AssignTask(Schedule model)
        {
            model.IsPublic = false;

            context.Schedules.Add(model);
            await context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult MyTask()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> MyTask(Schedule model)
        {
            var userId = Guid.Parse(User.FindFirst("sub")!.Value);

            model.UserId = userId;
            model.Type = "Personal";
            model.IsPublic = false;

            context.Schedules.Add(model);
            await context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
