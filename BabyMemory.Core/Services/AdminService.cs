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
                Username = x.UserName
            }).ToListAsync();

            return users;
        }
    }
}
