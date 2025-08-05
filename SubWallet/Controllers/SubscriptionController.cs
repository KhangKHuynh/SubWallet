using Microsoft.AspNetCore.Mvc;
using SubWallet.Models;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class SubscriptionsController : Controller
    {
        private readonly AppDbContext _context;

        public SubscriptionsController(AppDbContext context)
        {
            _context = context;
        }

        // Show list of subscriptions
        public IActionResult Index()
        {
            var subscriptions = _context.Subscriptions.ToList();
            return View(subscriptions);
        }

        // Show form to add a subscription
        public IActionResult Add()
        {
            return View();
        }

        // Handle form submission
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

        // Later: Add Edit/Delete/Calendar
    }
}