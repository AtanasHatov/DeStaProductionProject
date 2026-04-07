using DeStaProduction.Core.Contracts;
using DeStaProduction.Infrastucture.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DeStaProduction.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<DeStaUser> userManager;
        private readonly ApplicationDbContext context;
        private readonly IScheduleService scheduleService;

        public AdminController(
            UserManager<DeStaUser> userManager,
            ApplicationDbContext context,
            IScheduleService scheduleService)
        {
            this.userManager = userManager;
            this.context = context;
            this.scheduleService = scheduleService;
        }

        public async Task<IActionResult> Index()
        {
            var users = userManager.Users.ToList();

            var model = new List<(DeStaUser user, IList<string> roles)>();

            foreach (var user in users)
            {
                var roles = await userManager.GetRolesAsync(user);
                model.Add((user, roles));
            }

            return View(model);
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

        public async Task<IActionResult> AssignTask()
        {
            var users = context.Users.ToList();
            var artists = new List<DeStaUser>();

            foreach (var user in users)
            {
                if (await userManager.IsInRoleAsync(user, "Artist"))
                {
                    artists.Add(user);
                }
            }

            ViewBag.Actors = new SelectList(artists, "Id", "UserName");
            ViewBag.Performances = new SelectList(context.Performances, "Id", "Title");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AssignTask(Schedule model)
        {
            if (!await scheduleService.IsUserAvailable(model.UserId, model.Date))
            {
                var actor = await userManager.FindByIdAsync(model.UserId.ToString());

                ModelState.AddModelError("", $"Актьорът {actor.FirstName} {actor.LastName} е зает!");

                var users = context.Users.ToList();
                var artists = new List<DeStaUser>();

                foreach (var user in users)
                {
                    if (await userManager.IsInRoleAsync(user, "Artist"))
                    {
                        artists.Add(user);
                    }
                }

                ViewBag.Actors = new SelectList(artists, "Id", "UserName");
                ViewBag.Performances = new SelectList(context.Performances, "Id", "Title");

                return View(model);
            }

            model.IsPublic = model.Type == "Performance";

            await scheduleService.AddTaskAsync(model);

            return RedirectToAction("Index", "Schedule", new
            {
                month = model.Date.Month,
                year = model.Date.Year
            });
        }

        public IActionResult MyTask()
        {
            ViewBag.Performances = new SelectList(context.Performances, "Id", "Title");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> MyTask(Schedule model)
        {
            var user = await userManager.GetUserAsync(User);
            var userId = user.Id;

            model.UserId = userId;
            model.Type = "Personal";
            model.IsPublic = false;

            context.Schedules.Add(model);
            await context.SaveChangesAsync();

            return RedirectToAction("Index", "Schedule", new
            {
                month = model.Date.Month,
                year = model.Date.Year
            });
        }
    }
}