using BabyMemory.Infrastructure.Data.Models;
using BabyMemory.Infrastructure.Models;

namespace BabyMemory.Core.Contracts
{
    public interface IAdminService
    {
        Task<List<UserNameViewModel>> GetAllUsersAsync();

        Task EditUserAsync(User user);
    }
}
