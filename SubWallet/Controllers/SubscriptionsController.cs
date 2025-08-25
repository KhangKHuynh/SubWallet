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
            {
                // Convert all costs to monthly equivalents
                foreach (var sub in subscriptions)
                {
                    decimal monthlyCost = sub.Cost;
                    switch (sub.Cycle)
                    {
                        case BillingCycle.Weekly:
                            monthlyCost = sub.Cost * 52 / 12m; // ~4.33 weeks per month
                            break;
                        case BillingCycle.BiWeekly:
                            monthlyCost = sub.Cost * 26 / 12m; // ~2.17 cycles per month
                            break;
                        case BillingCycle.Monthly:
                            monthlyCost = sub.Cost;
                            break;
                        case BillingCycle.Yearly:
                            monthlyCost = sub.Cost / 12m;
                            break;
                    }

                    // store the converted cost temporarily for chart
                    sub.Cost = Math.Round(monthlyCost, 2);
                }

                return View(subscriptions);
            }

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
        [HttpPost]
        public IActionResult Delete(string name)
        {
            var sub = _context.Subscriptions.FirstOrDefault(s => s.Name == name);
            if (sub != null)
            {
                _context.Subscriptions.Remove(sub);
                _context.SaveChanges();
            }

            // Redirect back to index so the chart refreshes
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        public IActionResult Edit(string name, decimal cost, BillingCycle cycle)
        {
            var sub = _context.Subscriptions.FirstOrDefault(s => s.Name == name);
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