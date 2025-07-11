using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

public class ProfileController : Controller
{
    public IActionResult Profile()
    {
        return View();
    }
}
