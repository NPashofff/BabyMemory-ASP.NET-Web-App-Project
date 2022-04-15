#nullable disable
namespace BabyMemory.Controllers
{
    using Infrastructure.Shared;
    using Microsoft.AspNetCore.Authorization;
    using Core.Contracts;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        
        //Create First Admin UserRoles
        public async Task<IActionResult> CreateFirstAdmin()
        {
            var role = await _userService.FindRoleByNameAsync(GlobalConstants.Administrator);
            if (User.Identity?.Name == null) return Ok();
            var user = await _userService.GetUserAsync(User.Identity?.Name);
            if (role != null) await _userService.CreateUserRoleAsync(user.Id, role.Id);

            return Ok();
        }
    }
}
