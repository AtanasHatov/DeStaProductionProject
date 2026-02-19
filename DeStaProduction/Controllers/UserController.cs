using DeStaProduction.Infrastucture.Entities;
using DeStaProduction.ViewModels.UserViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DeStaProduction.Controllers
{
    [AllowAnonymous]
    public class UserController : Controller
    {
        private readonly UserManager<DeStaUser> userManager;
        private readonly SignInManager<DeStaUser> signInManager;
        private readonly RoleManager<IdentityRole<Guid>> roleManager;
        public UserController(UserManager<DeStaUser> _userManager, SignInManager<DeStaUser> _signInManager, RoleManager<IdentityRole<Guid>> _roleManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            roleManager=_roleManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new RegisterViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult>Register(RegisterViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            var user = new DeStaUser()
            {
                Email = model.Email,
                UserName = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                EmailConfirmed = true
            };
            var result = await userManager.CreateAsync(user, model.ConfirmPassword);
            if (result.Succeeded)
            {
                if(model.Role=="Artist" || model.Role == "User")
                {
                    await userManager.AddToRoleAsync(user, model.Role);
                }
                return RedirectToAction("Login", "User");
            }
            foreach(var item in result.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            if(User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("Index","Home");
            }
            var model = new LoginViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await userManager.FindByNameAsync(model.Email);
            if (!user.IsApproved)
            {
                ModelState.AddModelError("", "Your account is awaiting administrator approval.");
                return View(model);
            }

            if (user != null)
            {
                var result = await signInManager.PasswordSignInAsync(user, model.Password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

            }
            ModelState.AddModelError("", "Invalid login");
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public async Task<IActionResult> SeedRoldes()
        {
            string[] roles = { "Admin", "Artist", "User" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole<Guid>(role));
                }
            }
            return Content("Roles seeded (created if missing).");
        }
    }
}
