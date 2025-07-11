namespace WebApplication1.Controllers;
using Microsoft.AspNetCore.Mvc;
using SubWallet.Models;

[ApiController]
[Route("api/[controller]")]
public class SubscriptionsController : ControllerBase
{
    // For simplicity, using in-memory list (replace with DB logic)
    private static List<Subscription> _subscriptions = new();

    [HttpPost("add")]
    public IActionResult AddSubscription([FromBody] Subscription subscription)
    {
        // You would usually save to DB here
        _subscriptions.Add(subscription);

        return Ok(new { message = "Subscription added", subscription });
    }
}
public class SubscriptionApiController : Controller
{
    public IActionResult Subscription()
    {
        return View();
    }
}