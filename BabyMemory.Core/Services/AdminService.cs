namespace BabyMemory.Core.Services
{
    using Contracts;
    using Infrastructure.Data;
    using Infrastructure.Data.Models;
    using Infrastructure.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class AdminService : IAdminService
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;

        public AdminService(ApplicationDbContext context,
            RoleManager<IdentityRole> roleManager,
            UserManager<User> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<List<UserNameViewModel>> GetAllUsersAsync()
        {
            var users = await _context.Users.Select(x => new UserNameViewModel
            {
                Id = x.Id,
                Username = x.UserName,
                Roles = _context.UserRoles.Where(r => r.UserId == x.Id).Select(r => r.RoleId).ToList()
            }).ToListAsync();

            return users;
        }

       

        public async Task EditUserAsync(User user)
        {
            var userToEdit = await _context.Users.FirstOrDefaultAsync(x => x.UserName == user.UserName);

            userToEdit.Picture = user.Picture;
            userToEdit.UserFullName = user.UserFullName;
            userToEdit.UserName = user.UserName;
            userToEdit.Email = user.Email;

            _context.Users.Update(userToEdit);
            await _context.SaveChangesAsync();
        }
    }
}
