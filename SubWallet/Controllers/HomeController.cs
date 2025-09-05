using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace SubWallet.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger; //not used yet 

    public HomeController(ILogger<HomeController> logger) //not used yet 
    {
        _logger = logger;
    }
    
    public IActionResult Logger()
    {
        _logger.LogInformation("Home page visited at {Time}", DateTime.Now);
        return View();
    }

    public IActionResult Index()
    {
        return View();
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}