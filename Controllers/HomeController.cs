using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Projeto_Final.Models;

namespace Projeto_Final.Controllers;

public class HomeController : Controller
{
    private readonly MyDbContext _context;
    private readonly ILogger<HomeController> _logger;

    public HomeController(MyDbContext context, ILogger<HomeController> logger)
    {
        _context = context;
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View(_context.Produto.ToList());
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
