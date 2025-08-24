using Microsoft.AspNetCore.Mvc;
using SubWallet.Models;
using WebApplication1.Models;

namespace SubWallet.Controllers
{
    public class SubscriptionsController : Controller
    {
        private readonly AppDbContext _context;

        public SubscriptionsController(AppDbContext context)
        {
            _context = context;
        }

        // MVC route for /subscriptions
        public IActionResult Index()
        {
            var subscriptions = _context.Subscriptions.ToList();
            return View(subscriptions);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Subscription subscription)
        {
            if (ModelState.IsValid)
            {
                _context.Subscriptions.Add(subscription);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(subscription);
        }
    }
}