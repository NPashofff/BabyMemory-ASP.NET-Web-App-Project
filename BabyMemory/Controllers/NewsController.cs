using BabyMemory.Data;
using BabyMemory.Data.Models;
using BabyMemory.Models;
using Microsoft.AspNetCore.Mvc;
using SharedTrip.Shared;

namespace BabyMemory.Controllers
{
    public class NewsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult News()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/");
            }

            var models = _context.News
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

            _context.News.Add(news);
            _context.SaveChanges();

            return Redirect("/News");
        }
    }
}
