using BabyMemory.Contracts;
using BabyMemory.Data;
using BabyMemory.Data.Models;

namespace BabyMemory.Controllers
{
    public class UserController : IUserController
    {
        private ApplicationDbContext context;

        public UserController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void SetUserFullName(User user, string fullName)
        {
            user.UserFullName = fullName;
            context.SaveChangesAsync();
        }

        public void SetUserDate(User user)
        {
            user.RegisterDate = DateTime.Now;
            context.SaveChangesAsync();
        }
    }
}
