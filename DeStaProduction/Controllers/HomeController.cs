using DeStaProduction.Infrastucture.Entities;
using DeStaProduction.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DeStaProduction.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext context;
    private readonly ILogger<HomeController> _logger;

    public HomeController
        (ILogger<HomeController> logger,
        ApplicationDbContext context)
    {
        _logger = logger;
        this.context = context;
    }

    [AllowAnonymous]
    public IActionResult Index()
    {
        var performances = context.Performances
            .OrderByDescending(p => p.Date)
            .Take(6)
            .ToList();

        return View(performances);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
