using BabyMemory.Core.Contracts;
using BabyMemory.Infrastructure.Models;

namespace BabyMemory.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;

    public class HomeController : Controller
    {
        private readonly IEventService _eventService;

        public HomeController(IEventService eventService)
        {
            _eventService = eventService;
        }

        public async Task<IActionResult> Index()
        {
            var events =await _eventService.GetAllActiveEventsAsync();
            
            return View(events);
        }

        //public async Task<IActionResult> Edit()
        //{
        //    return View();
        //}

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View( Activity.Current?.Id ?? HttpContext.TraceIdentifier );
        }
    }
}