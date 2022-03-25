using BabyMemory.Core.Contracts;
using BabyMemory.Infrastructure.Data;
using BabyMemory.Infrastructure.Data.Models;
using BabyMemory.Infrastructure.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BabyMemory.Core.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;

        public UserService(ApplicationDbContext context,
            RoleManager<IdentityRole> roleManager,
            UserManager<User> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public Task SetUserFullNameAsync(User user, string inputUserFullName)
        {
            user.UserFullName = inputUserFullName;
            _context.SaveChangesAsync();

            return Task.CompletedTask;
        }

        public Task SetUserDateAsync(User user)
        {
            user.RegisterDate = DateTime.Now;
            _context.SaveChangesAsync();

            return Task.CompletedTask;
        }

        public async Task<User?> GetUserAsync(string name)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == name);

            return user;
        }

        public async Task CreateRoleAsync(string administrator)
        {
            await _roleManager.CreateAsync(new IdentityRole
            {
                Name = administrator
            });
        }

        public async Task<IdentityRole?> FindRoleByNameAsync(string administrator)
        {
            var role = await _roleManager.FindByNameAsync(administrator);

            return role;
        }

        public async Task CreateUserRoleAsync(string userId, string roleId)
        {
            await _context.UserRoles.AddAsync(new IdentityUserRole<string>
            {
                UserId = userId,
                RoleId = roleId
            });
            await _context.SaveChangesAsync();
        }
    }
}
