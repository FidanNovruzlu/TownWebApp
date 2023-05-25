using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TownWebApp.DAL;
using TownWebApp.Models;
using TownWebApp.ViewModels;

namespace TownWebApp.Controllers;

public class HomeController : Controller
{
    private readonly TownDbContext _context;
    public HomeController(TownDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        List<Introdaction> introdactions = _context.Introdactions.Take(3).ToList();
        HomeVM homeVM = new HomeVM()
        {
           Introdactions = introdactions
        };
        return View(homeVM);
    }
}