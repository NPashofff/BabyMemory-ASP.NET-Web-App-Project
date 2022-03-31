using BabyMemory.Core.Contracts;
using BabyMemory.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace BabyMemory.Controllers
{
    public class MedicineController : Controller
    {
        private readonly IMedicineService _medicineService;

        public MedicineController(IMedicineService medicineService)
        {
            _medicineService = medicineService;
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(MedicineAddViewModel model)
        {

            //TODO ..............


            return View();
        }
    }
}
