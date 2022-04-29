namespace BabyMemory.Controllers
{
    using Core.Contracts;
    using Infrastructure.Data.Models;
    using Infrastructure.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class MemoryController : Controller
    {
        private readonly IMemoryService _memoryService;
        private readonly UserManager<User> _userManager;

        public MemoryController(IMemoryService memoryService, UserManager<User> userManager)
        {
            _memoryService = memoryService;
            _userManager = userManager;
        }

        public IActionResult All()
        {
            return View();
        }

        public IActionResult Add(string childrenId)
        {
            var model = new MemoryAddViewModel()
            {
                ChildId = childrenId,
                CreationDate = DateTime.Now
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(MemoryAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await _userManager.GetUserAsync(User);
                await _memoryService.AddMemory(model, currentUser);
                return Redirect("/Children/Profile?id=" + model.ChildId);
            }

            return View(model);
        }

        public async Task<IActionResult> Edit(string Id)
        {
            var memory = await _memoryService.GetMemoryAsync(Id);

            return View(memory);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Memory model, string submitButton)
        {
            switch (submitButton)
            {
                case "Edit":
                    await _memoryService.Edit(model);
                    break;
                case "Delete":
                    await _memoryService.DeleteAsync(model);
                    break;
            }

            return Redirect("/Children/All");
        }
    }
}
