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

        // ✅ Helper method to normalize subscription costs into monthly values
        private List<Subscription> GetNormalizedSubscriptions()
        {
            var subscriptions = _context.Subscriptions.ToList();

            foreach (var sub in subscriptions)
            {
                decimal monthlyCost = sub.Cost;

                switch (sub.Cycle)
                {
                    case BillingCycle.Weekly:
                        monthlyCost = sub.Cost * 52 / 12m;
                        break;
                    case BillingCycle.BiWeekly:
                        monthlyCost = sub.Cost * 26 / 12m;
                        break;
                    case BillingCycle.Monthly:
                        monthlyCost = sub.Cost;
                        break;
                    case BillingCycle.Yearly:
                        monthlyCost = sub.Cost / 12m;
                        break;
                }

                sub.Cost = Math.Round(monthlyCost, 2);
            }

            return subscriptions;
        }

        
        public IActionResult Index()
        {
            var model = new SubscriptionsViewModel
            {
                Subscriptions = GetNormalizedSubscriptions(),
                NewSubscription = new Subscription() // for the "Add" form
            };

            return View(model);
        }

        // ✅ Add Subscription
        [HttpPost]
        public IActionResult Add(SubscriptionsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var subscription = model.NewSubscription;

                // Prevent duplicate names
                bool exists = _context.Subscriptions
                    .Any(s => s.Name.ToLower() == subscription.Name.ToLower());

                if (exists)
                {
                    ModelState.AddModelError("NewSubscription.Name", "A subscription with this name already exists.");
                    model.Subscriptions = GetNormalizedSubscriptions();
                    return View("Index", model);
                }

                _context.Subscriptions.Add(subscription);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            // Re-populate subscriptions if validation fails
            model.Subscriptions = GetNormalizedSubscriptions();
            return View("Index", model);
        }

        // ✅ Delete Subscription
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var sub = _context.Subscriptions.FirstOrDefault(s => s.ID == id);

            if (sub != null)
            {
                _context.Subscriptions.Remove(sub);
                _context.SaveChanges();
            }

            return RedirectToAction("Index"); // Chart will always refresh
        }

        // ✅ Edit Subscription
        [HttpPost]
        public IActionResult Edit(int id, decimal cost, BillingCycle cycle)
        {
            var sub = _context.Subscriptions.FirstOrDefault(s => s.ID == id);

            if (sub != null)
            {
                sub.Cost = cost;
                sub.Cycle = cycle;
                _context.SaveChanges();
            }

            return RedirectToAction("Index"); 
        }
    }
}
