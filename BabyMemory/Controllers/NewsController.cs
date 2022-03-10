namespace BabyMemory.Controllers
{
    using Data;
    using Data.Models;
    using Models;
    using Microsoft.AspNetCore.Mvc;
    using SharedTrip.Shared;

    public class NewsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult All()
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

        public IActionResult DeleteNews(string id)
        {
            var news = _context.News.FirstOrDefault(x => x.Id == id);
            _context.News.Remove(news);
            _context.SaveChanges();
            return Redirect("/News/All");
        }
    }
}
