namespace BabyMemory.Controllers
{
    using Infrastructure.Models;
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
        private readonly IEventService _eventService;


        public AdministratorController(IAdminService adminService,
            IUserService userService,
            IEventService eventService)
        {
            _adminService = adminService;
            _userService = userService;
            _eventService = eventService;
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

        public async Task<IActionResult> AddUserToAdmin(string userName)
        {
            var user = await _userService.GetUserAsync(userName);
            var role = await _userService.FindRoleByNameAsync(GlobalConstants.Administrator);
            if (user != null && role != null)
                await _userService.CreateUserRoleAsync(user, role);

            return Redirect("/Administrator/AllUsers");
        }

        public async Task<IActionResult> RemoveUserFromAdmin(string userName)
        {
            var user = await _userService.GetUserAsync(userName);
            var role = await _userService.FindRoleByNameAsync(GlobalConstants.Administrator);
            if (user != null && role != null)
                await _userService.RemoveUserRoleAsync(user, role);

            return Redirect("/Administrator/AllUsers");
        }
        
        public async Task<IActionResult> AllEvents()
        {
            ICollection<EventViewModel> users = await _eventService.GetAllEventsAsync();

            return View(users);
        }

        public async Task<IActionResult> AllMedicine()
        {
            var medicines = await _adminService.GetAllMedicineAsync();

            return View(medicines);
        }

        public async Task<IActionResult> RemoveMedicine(string id)
        {
            await _adminService.RemoveMedicineAsync(id);

            return Redirect("/Administrator/AllMedicine");
        }
    }
}
