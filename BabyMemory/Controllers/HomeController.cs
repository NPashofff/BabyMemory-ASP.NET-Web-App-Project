﻿using BabyMemory.Models;
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
        private ApplicationDbContext context;

        public HomeController(ILogger<HomeController> logger,
            ApplicationDbContext context)
        {
            this.logger = logger;
            this.context = context;
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
                .Where(x=>x.IsActive == true)
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
                
            }

            var news = new News
            {
                Name = model.Name,
                Description = model.Description
            };

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}