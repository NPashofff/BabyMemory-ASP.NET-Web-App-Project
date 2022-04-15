using BabyMemory.Infrastructure.Data.Models;

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

        public async Task<IActionResult> MyEvents()
        {
            var events = await _eventService.GetMyEventsAsync(User.Identity.Name);

            return View(events);
        }

        public async Task<IActionResult> Edit(string eventId)
        {
            var eventToEdit = await _eventService.GetEventByIdAsync(eventId);

            return View(eventToEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Event model)
        {
           await _eventService.EditEventAsync(model);

            return Redirect("/");
        }

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

        public async Task<IActionResult> Remove(string eventId)
        {
            await _eventService.DeleteEventAsync(eventId);

            return Redirect("/");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(Activity.Current?.Id ?? HttpContext.TraceIdentifier);
        }
    }
}