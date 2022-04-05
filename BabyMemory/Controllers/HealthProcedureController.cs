using BabyMemory.Core.Contracts;
using BabyMemory.Infrastructure.Data.Models;
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

        public async Task<IActionResult> Add(string childId)
        {
            ViewBag.ChildId = childId;
            List<Medicine> medicines = await _healthProcedureService.GetAllMedicinesAsync();
            ViewBag.Medicines = medicines;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(HealthProcedureViewModel model, string childId, List<Medicine> Medicines)
        {

            //TODO ..............


            return View();
        }
    }
}
