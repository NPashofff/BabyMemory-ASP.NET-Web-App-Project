﻿#nullable disable
namespace BabyMemory.Controllers
{
    using Infrastructure.Shared;
    using Core.Contracts;
    using Infrastructure.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class ChildrenController : Controller
    {
        private readonly IChildrenService _childrenService;

        public ChildrenController(IChildrenService childrenService)
        {
            _childrenService = childrenService;
        }

        public async Task<IActionResult> All()
        {
            string name = User.Identity.Name;
            ChildrenViewModel[] children = await _childrenService.All(name);
            
            return View(children);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(ChildrenAddViewModel model)
        {
            if (model.BirthDate > DateTime.Now) ModelState
                .AddModelError(nameof(model.BirthDate),
                    GlobalConstants.BirthDateError);

            if (!ModelState.IsValid) return View(model);

            await _childrenService.AddChildren(model, User.Identity.Name);

            return Redirect("/Children/All");
        }

        public async Task<IActionResult> Delete(string childrenId)
        {
            await _childrenService.Delete(childrenId);

            return Redirect("/Children/All");
        }

        public async Task<IActionResult> Profile(string id)
        {
            var result = await _childrenService.GetChildren(id);

            return View(result);
        }

        public async Task<IActionResult> Edit(string childrenId)
        {
            var result = await _childrenService.GetChildren(childrenId);

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ChildrenViewModel model)
        {
            if (model.BirthDate > DateTime.Now) ModelState
                .AddModelError(nameof(model.BirthDate),
                    GlobalConstants.BirthDateError);

            if (!ModelState.IsValid) return View(model);

            await _childrenService.Edit(model);

            return Redirect("/Children/Profile/" + model.Id);
        }
    }
}
