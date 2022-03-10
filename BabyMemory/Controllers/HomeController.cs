using BabyMemory.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using BabyMemory.Data;
using BabyMemory.Data.Models;
using SharedTrip.Shared;

namespace BabyMemory.Controllers
{
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

        public IActionResult News()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/");
            }

            var models = context.News
                .Where(x => x.IsActive == true)
                .Select(x => new NewsViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Picture = GlobalConstants.NewsLogo,
                    IsActive = x.IsActive
                }).ToArray();

            return View(models);
        }

        public IActionResult AddNews()
        {
            //TODO: User Admin
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/");
            }

            return View();
        }

        [HttpPost]
        public IActionResult AddNews(AddNewsViewModel model)
        {
            //TODO:Admin
             if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/");
            }

            if (!ModelState.IsValid)
            {
                return View("/Views/Shared/Error.cshtml");
            }

            var news = new News
            {
               Name = model.Name,
                Description = model.Description
            };

            context.News.AddAsync(news);

            context.SaveChangesAsync();

            return Redirect("/Home/News");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}