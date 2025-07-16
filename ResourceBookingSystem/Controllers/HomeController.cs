using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ResourceBookingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace ResourceBookingSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        /// <summary>
        /// Get: Home/Index displays a list of all bookings with related resources.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            var bookings = _context.Bookings
                .Include(b => b.Resource) // Eagerly load the related Resource
                .ToList();
            return View(bookings);
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
        /// <summary>
        /// creates a dashboard view that shows today's bookings.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Dashboard()
        {
            var today = DateTime.Today;
            var bookings = await _context.Bookings
                .Include(b => b.Resource)
                .Where(b => b.StartTime.Date == today || b.StartTime > today)
                .OrderBy(b => b.StartTime)
                .ToListAsync();
            return View(bookings);
        }
    }
}
