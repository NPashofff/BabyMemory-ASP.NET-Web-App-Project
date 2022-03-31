using BabyMemory.Core.Contracts;
using BabyMemory.Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BabyMemory.Controllers
{
    [Authorize]
    public class HealthProcedureController : Controller
    {
        private readonly IHealthProcedureService _healthProcedureService;

        public HealthProcedureController(IHealthProcedureService healthProcedureService)
        {
            _healthProcedureService = healthProcedureService;
        }

        public IActionResult Add(string childId)
        {
            ViewBag.ChildId = childId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(HealthProcedureViewModel model, string childId)
        {
            
            //TODO ..............


            return View();
        }
    }
}
