﻿#nullable disable
namespace BabyMemory.Controllers
{
    using Contracts;
    using Infrastructure.Data;
    using Infrastructure.Data.Models;

    public class UserController : IUserController
    {
        private readonly ApplicationDbContext context;

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
