#nullable disable
namespace BabyMemory.Controllers
{
    using BabyMemory.Core.Contracts;
    using Microsoft.AspNetCore.Authorization;
    using Infrastructure.Data;
    using Infrastructure.Models;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class NewsController : Controller
    {
        private readonly INewsService _newsService;

        public NewsController(ApplicationDbContext context,
            INewsService newsService)
        {
            _newsService = newsService;
        }

        public IActionResult All()
        {
            var models = _newsService.GetAllNews();

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
                return View("Error");
            }

            (bool, string) result = _newsService.AddNews(model);

            if (result.Item1)
            {
                return Redirect("/News/All");
            }
            else
            {
                return View("Error", result.Item2);
            }
        }

        public IActionResult DeleteNews(string id)
        {
            (bool, string) result = _newsService.DeleteNews(id);

            if (result.Item1)
            {
                return Redirect("/News/All");
            }
            else
            {
                return View("Error", result.Item2);
            }
        }
    }
}
