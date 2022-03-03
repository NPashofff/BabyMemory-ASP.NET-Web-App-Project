using BabyMemory.Data.Models;

namespace BabyMemory.Contracts
{
    public interface IUserController
    {
        public void SetUserFullName(User user, string fullName);
    }
}
