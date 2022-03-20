using System.Security.Claims;
using BabyMemory.Core.Contracts;
using BabyMemory.Infrastructure.Data.Models;
using BabyMemory.Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BabyMemory.Controllers
{
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
                _memoryService.AddMemory(model, currentUser);
                return Redirect("/Children/Profile?id=" + model.ChildId);
            }

            return View();
        }

        public async Task<IActionResult> Edit(string Id)
        {
            Memory memory = await _memoryService.GetMemoryAsync(Id);

            return View(memory);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Memory model, string submitButton)
        {
            if (submitButton == "Edit")
            {
                await _memoryService.Edit(model);
            }

            if (submitButton == "Delete")
            {
                await _memoryService.DeleteAsync(model);
            }

            return Redirect("/Children/All");
        }

        
    }
}
