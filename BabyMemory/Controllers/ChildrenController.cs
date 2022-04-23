#nullable disable
namespace BabyMemory.Controllers
{
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
            if (ModelState.IsValid)
            {
                await _childrenService.AddChildren(model, User.Identity.Name);
                return Redirect("/Children/All");
            }

            return View();
        }

        public async Task<IActionResult> Delete(string id)
        {
            await _childrenService.Delete(id);
            
            return Redirect("/Children/All");
        }

        public async Task<IActionResult> Profile(string id)
        {
            var result =await _childrenService.GetChildren(id);

            return View(result);
        }
    }
}
