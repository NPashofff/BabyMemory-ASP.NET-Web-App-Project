
namespace BabyMemory.Controllers
{
    using Core.Contracts;
    using Infrastructure.Data.Models;
    using Infrastructure.Shared;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    
    [Authorize(Roles = GlobalConstants.Administrator)]
    public class AdministratorController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly IUserService _userService;


        public AdministratorController(IAdminService adminService, IUserService userService)
        {
            _adminService = adminService;
            _userService = userService;
        }

        public async Task<IActionResult> AllUsers()
        {
            var users = await _adminService.GetAllUsersAsync();



            return View(users);
        }

        public async Task<IActionResult> EditUser(string userName)
        {

            var user = await _userService.GetUserAsync(userName);

            return View(user);
        }


        [HttpPost]
        public async Task<IActionResult> EditUser(User user)
        {
            await _adminService.EditUserAsync(user);


            return Redirect("/Administrator/AllUsers");
        }

    }
}
