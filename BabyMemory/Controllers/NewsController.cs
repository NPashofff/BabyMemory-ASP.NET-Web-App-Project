using Microsoft.AspNetCore.Authorization;

#nullable disable
namespace BabyMemory.Controllers
{
    using BabyMemory.Infrastructure.Data.Models;
    using BabyMemory.Infrastructure.Data;
    using BabyMemory.Infrastructure.Shared;
    using Microsoft.AspNetCore.Mvc;
    using Infrastructure.Models;

    [Authorize]
    public class NewsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult All()
        {
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
            
            return View();
        }

        [HttpPost]
        public IActionResult AddNews(AddNewsViewModel model)
        {
            //TODO:Admin
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

            return Redirect("/News/All");
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
