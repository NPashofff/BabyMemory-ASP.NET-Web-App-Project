namespace BabyMemory.Controllers
{
    using Core.Contracts;
    using Infrastructure.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;

    [Authorize]
    public class HomeController : Controller
    {
        private readonly IEventService _eventService;

        public HomeController(IEventService eventService)
        {
            _eventService = eventService;
        }

        public async Task<IActionResult> Index()
        {
            var events = await _eventService.GetAllActiveEventsAsync();

            return View(events);
        }

        //public async Task<IActionResult> Edit()
        //{
        //    return View();
        //}

        public IActionResult Create()
        {
            EventViewModel model = new EventViewModel()
            {
                CreationDate = DateTime.Now,
                EventDate = DateTime.Now,
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(EventViewModel model)
        {
            await _eventService.CreateEventAsync(model, User.Identity.Name);

            return Redirect("/");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(Activity.Current?.Id ?? HttpContext.TraceIdentifier);
        }
    }
}