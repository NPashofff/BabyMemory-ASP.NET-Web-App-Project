using BabyMemory.Core.Contracts;
using BabyMemory.Infrastructure.Models;
using BabyMemory.Infrastructure.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BabyMemory.Controllers
{
    [Authorize(Roles = GlobalConstants.Administrator)]
    public class AdministratorController : Controller
    {
        private readonly IAdminService _adminService;

        public AdministratorController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<IActionResult> AllUsers()
        {
           List<UserNameViewModel> users = await _adminService.GetAllUsersAsync();
            
            return View(users);
        }
    }
}
