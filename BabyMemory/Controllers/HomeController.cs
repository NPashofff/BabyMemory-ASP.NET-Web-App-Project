namespace BabyMemory.Controllers
{
    using Data;
    using Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly ApplicationDbContext context;

        public HomeController(ILogger<HomeController> _logger,
            ApplicationDbContext _context)
        {
            this.logger = _logger;
            this.context = _context;
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
}