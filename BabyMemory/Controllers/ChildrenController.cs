using BabyMemory.Core.Contracts;
using BabyMemory.Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            return View();
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
                _childrenService.AddChildren(model);

            }
            return View();
        }
    }
}
