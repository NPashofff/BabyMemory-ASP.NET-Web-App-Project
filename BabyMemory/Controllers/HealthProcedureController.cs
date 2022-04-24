namespace BabyMemory.Controllers
{
    using Core.Contracts;
    using Infrastructure.Data.Models;
    using Infrastructure.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class HealthProcedureController : Controller
    {
        private readonly IHealthProcedureService _healthProcedureService;

        public HealthProcedureController(IHealthProcedureService healthProcedureService)
        {
            _healthProcedureService = healthProcedureService;
        }

        public async Task<IActionResult> Add(string childId)
        {
            ViewBag.ChildId = childId;
            List<Medicine> medicines = await _healthProcedureService.GetAllMedicinesAsync();
            ViewBag.Medicines = medicines;
            HealthProcedureViewModel model = new();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(HealthProcedureViewModel model, string childId)
        {

            await _healthProcedureService.AddHealthProcedureAsync(model, childId);


            return Redirect("/Children/Profile?id=" + childId);
        }
    }
}
