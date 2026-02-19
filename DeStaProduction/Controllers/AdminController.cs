using DeStaProduction.Infrastucture.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DeStaProduction.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<DeStaUser> userManager;

        public AdminController(UserManager<DeStaUser> userManager)
        {
            this.userManager = userManager;
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
    }
}
