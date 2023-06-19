using System.Diagnostics;
using learningGate.Data;
using learningGate.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using learningGate.Data;
using learningGate.Models;
using learningGate.ViewModels;

namespace learningGate.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly learningGateDbContext _context;


    public HomeController(ILogger<HomeController> logger,learningGateDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var learningGateDbContext = _context.Products.Include(p => p.ProductType).Include(p => p.Images);
        var homeView = new HomeViewModel()
        {
            Products = await learningGateDbContext.ToListAsync()
        };
        return View(homeView);
    }

    public IActionResult Privacy()
    {
        return View();
    }
    public IActionResult About()
    {
        return View();
    }
    public IActionResult Contact()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

