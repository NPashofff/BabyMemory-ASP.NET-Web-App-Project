namespace BabyMemory.Core.Services
{
    using Contracts;
    using Infrastructure.Data;
    using Infrastructure.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

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

        public async Task<IdentityRole?> FindRoleByNameAsync(string name)
        {
            var role = await _roleManager.FindByNameAsync(name);

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

        public async Task RemoveUserRoleAsync(string userId, string roleId)
        {
            var role = await _context.UserRoles
                .FirstOrDefaultAsync(x => x.RoleId == roleId && x.UserId == userId);
            _context.UserRoles.Remove(role);
            await _context.SaveChangesAsync();
        }
    }
}
