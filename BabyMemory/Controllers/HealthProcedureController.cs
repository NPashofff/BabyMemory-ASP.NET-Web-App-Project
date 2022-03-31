using BabyMemory.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace BabyMemory.Controllers
{
    public class HealthProcedureController : Controller
    {
        public IActionResult Add(string childId)
        {

            return View(childId);
        }

        [HttpPost]
        public async Task<IActionResult> Add(HealthProcedureViewModel model, string childId)
        {
            



            return View();
        }
    }
}
