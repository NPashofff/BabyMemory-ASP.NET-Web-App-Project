using BabyMemory.Core.Contracts;
using BabyMemory.Infrastructure.Data.Models;
using BabyMemory.Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
#nullable disable
namespace BabyMemory.Controllers
{
    [Authorize]
    public class ChildrenController : Controller
    {
        private readonly IChildrenService _childrenService;

        public ChildrenController(IChildrenService childrenService)
        {
            _childrenService = childrenService;
        }

        public IActionResult All()
        {
            string name = User.Identity.Name;

            ChildrenViewModel[] children = _childrenService.All(name);
            return View(children);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(ChildrenAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                _childrenService.AddChildren(model, User.Identity.Name);
                return Redirect("/Children/All");
            }

            return View();
        }

        public IActionResult Delete(string id)
        {
            (bool, string) result = _childrenService.Delete(id);

            if (result.Item1)
            {
                return Redirect("/Children/All");
            }

            return View("/Error", result.Item2);
        }

        public IActionResult Profile(string id)
        {
            var result = _childrenService.GetChildren(id);
            
            return View(result);
        }
    }
}
