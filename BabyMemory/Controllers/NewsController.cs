#nullable disable
namespace BabyMemory.Controllers
{
    using Infrastructure.Shared;
    using Core.Contracts;
    using Infrastructure.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class NewsController : Controller
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        public async Task<IActionResult> All()
        {
            var models =await _newsService.GetAllNews();

            return View(models);
        }

        [Authorize(Roles = GlobalConstants.Administrator)]
        public IActionResult AddNews()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.Administrator)]
        public IActionResult AddNews(AddNewsViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

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

        [Authorize(Roles = GlobalConstants.Administrator)]
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
