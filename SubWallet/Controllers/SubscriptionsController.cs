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
            return Ok(subscriptions);
        }

        // POST: api/subscriptions/add
        [HttpPost("add")]
        public IActionResult Add([FromBody] Subscription subscription)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Subscriptions.Add(subscription);
            _context.SaveChanges();

            return Ok(subscription);
        }
        
        // GET: api/subscriptions/calendar
        [HttpGet("calendar")]
        public IActionResult GetCalendarEvents()
        {
            var events = _context.Subscriptions.Select(s => new
            {
                title = s.Name + " - $" + s.Cost,
                start = s.StartDate.ToString("yyyy-MM-dd") // or use NextDate if you’ve got it
            }).ToList();

            return Ok(events);
        }
    }
}