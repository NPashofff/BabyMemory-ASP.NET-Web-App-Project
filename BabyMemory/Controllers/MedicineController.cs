namespace BabyMemory.Controllers
{
    using Core.Contracts;
    using Infrastructure.Models;
    using Microsoft.AspNetCore.Mvc;

    public class MedicineController : Controller
    {
        private readonly IMedicineService _medicineService;

        public MedicineController(IMedicineService medicineService)
        {
            _medicineService = medicineService;
        }

        public IActionResult Add(string childId)
        {
            ViewBag.CildId = childId;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(MedicineAddViewModel model, string childId)
        {
            await _medicineService.AddMedicineAsync(model);

            return Redirect($"/Children/Profile?id={childId}");
        }
    }
}
