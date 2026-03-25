using DeStaProduction.Infrastucture.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Threading.Tasks;

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

        public async Task<IActionResult> Index(int? month, int? year)
        {
            int m = month ?? DateTime.Now.Month;
            int y = year ?? DateTime.Now.Year;

            ViewBag.Month = m;
            ViewBag.Year = y;

            var query = context.Schedules
             .Include(s => s.User)
             .Include(s => s.Performance)
             .ThenInclude(p => p.Event)
             .AsQueryable();

            if (User.IsInRole("Artist"))
            {
                var user = await userManager.GetUserAsync(User);
                var userId = user.Id;
                query = query.Where(s => s.UserId == userId);
            }

            if (User.IsInRole("User"))
            {
                query = query.Where(s => s.Type == "Performance");
            }

            var schedules = query
                .Where(s => s.Date.Month == m && s.Date.Year == y)
                .ToList();

            return View(schedules);
        }
    }
}
