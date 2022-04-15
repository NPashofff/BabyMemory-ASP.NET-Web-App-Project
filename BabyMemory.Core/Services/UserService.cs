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

        public async Task SetUserFullNameAsync(User user, string inputUserFullName)
        {
            user.UserFullName = inputUserFullName;
            await _context.SaveChangesAsync();
        }

        public async Task SetUserDateAsync(User user)
        {
            user.RegisterDate = DateTime.Now;
            await _context.SaveChangesAsync();
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

        public async Task CreateUserRoleAsync(User user, IdentityRole role)
        {
           await _userManager.AddToRoleAsync(user, role.Name);
        }

        public async Task RemoveUserRoleAsync(User user, IdentityRole role)
        {
            await _userManager.RemoveFromRoleAsync(user, role.Name);
        }
    }
}
