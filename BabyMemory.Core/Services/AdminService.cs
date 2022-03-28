using BabyMemory.Core.Contracts;
using BabyMemory.Infrastructure.Data;
using BabyMemory.Infrastructure.Data.Models;
using BabyMemory.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BabyMemory.Core.Services
{
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
