using BabyMemory.Infrastructure.Shared;
using Microsoft.AspNetCore.Authorization;

namespace BabyMemory.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Core.Contracts;
    using Infrastructure.Data.Models;
    using Infrastructure.Models;

    [Authorize]
    public class EventController : Controller
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        public async Task<IActionResult> All()
        {
            var events = await _eventService.GetAllActiveEventsAsync();

            return View(events);
        }

        public async Task<IActionResult> MyEvents()
        {
            if (User.Identity == null)
            {
                return Redirect("/");
            }

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
            if (model.EventDate < DateTime.Now) ModelState
                .AddModelError(nameof(model.EventDate),
                    GlobalConstants.EventDateError);

            if (!ModelState.IsValid) return View(new EventViewModel
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                CreationDate = model.CreationDate,
                EventDate = model.EventDate,
                IsPublic = model.IsPublic
            });
            
            await _eventService.EditEventAsync(model);

            return Redirect("/Event/All");
        }

        public IActionResult Create()
        {
            EventViewModel model = new()
            {
                CreationDate = DateTime.Now,
                EventDate = DateTime.Now,
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(EventViewModel model)
        {
            if (User.Identity == null) return Redirect("/");
            if (User.Identity.Name == null) return Redirect("/");
            
            if (model.EventDate < DateTime.Now) ModelState
                .AddModelError(nameof(model.EventDate),
            GlobalConstants.EventDateError);

            if (!ModelState.IsValid) return View(model);

            await _eventService.CreateEventAsync(model, User.Identity.Name);

            return Redirect("/Event/All");
        }

        public async Task<IActionResult> Remove(string eventId)
        {
            await _eventService.DeleteEventAsync(eventId);

            return Redirect("/Event/All");
        }
    }
}
