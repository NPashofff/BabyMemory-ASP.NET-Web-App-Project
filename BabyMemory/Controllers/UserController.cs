#nullable disable
namespace BabyMemory.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Contracts;
    using Infrastructure.Data;
    using Infrastructure.Data.Models;

    public class UserController : Controller, IUserController
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;

        public UserController(ApplicationDbContext context,
            RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public void SetUserFullName(User user, string fullName)
        {
            user.UserFullName = fullName;
            _context.SaveChangesAsync();
        }

        public void SetUserDate(User user)
        {
            user.RegisterDate = DateTime.Now;
            _context.SaveChangesAsync();
        }


        //Create role
        public async Task<IActionResult> AddRole()
        {
            //await _roleManager.CreateAsync(new IdentityRole
            //{
            //    Name = "Administrator"
            //});

            return Ok();
        }


        //Create UserRoles
        public async Task<IActionResult> CreateAdmin()
        {
            //var role = await _roleManager.FindByNameAsync("Administrator");
            //var user = await _userManager.GetUserAsync(User);
            //await _context.UserRoles.AddAsync(new IdentityUserRole<string>
            //{
            //    UserId = user.Id,
            //    RoleId = role.Id
            //});
            //await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
