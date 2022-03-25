using BabyMemory.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace BabyMemory.Core.Contracts
{
    public interface IUserService
    {
        Task SetUserFullNameAsync(User user, string inputUserFullName);
        Task SetUserDateAsync(User user);
        Task<User?> GetUserAsync(string name);
        Task CreateRoleAsync(string administrator);
        Task<IdentityRole?> FindRoleByNameAsync(string administrator);
        Task CreateUserRoleAsync(string userId, string roleId);
    }
}
