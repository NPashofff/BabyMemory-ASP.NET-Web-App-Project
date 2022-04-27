namespace BabyMemory.Test
{
    using Core.Services;
    using Infrastructure.Data;
    using Infrastructure.Data.Models;
    using Microsoft.Extensions.DependencyInjection;
    using NUnit.Framework;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class UserServiceTest
    {
        private ServiceProvider serviceProvider;
        private InMemoryDbContext dbContext;

        [SetUp]
        public async Task Setup()
        {
            dbContext = new InMemoryDbContext();
            var serviceCollection = new ServiceCollection();

            serviceProvider = serviceCollection
                .AddSingleton(sp => dbContext.CreateContext())
                .AddDbContext<ApplicationDbContext>()
                .BuildServiceProvider();

            var repo = serviceProvider.GetService<ApplicationDbContext>();
            if (repo != null) await SeedDbAsync(repo);
        }

        [Test]
        public async Task SetUserFullNameTest()
        {
            var repo = serviceProvider.GetService<ApplicationDbContext>();
            var user = repo.Users.First();
            var userService = new UserService(repo, null, null);

            await userService.SetUserFullNameAsync(user, "New Name");

            Assert.AreEqual("New Name", user.UserFullName);
        }

        [Test]
        public async Task SetUserRegisterDateTest()
        {
            var repo = serviceProvider.GetService<ApplicationDbContext>();
            var user = repo.Users.First();
            var userService = new UserService(repo, null, null);

            var date = DateTime.Now;
            await userService.SetUserDateAsync(user);

            Assert.AreEqual(date.Day, user.RegisterDate.Day);
        }

        [Test]
        public async Task GetUserAsyncTest()
        {
            var repo = serviceProvider.GetService<ApplicationDbContext>();
            var userService = new UserService(repo, null, null);

            var user = await userService.GetUserAsync("pesho");

            Assert.AreEqual("pesho", user.UserName);
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }

        private async Task SeedDbAsync(ApplicationDbContext repo)
        {
            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "pesho",
                Email = "Pesho@pesho.com",
                UserFullName = "null",
            };

            await repo.AddAsync(user);
            await repo.SaveChangesAsync();
        }
    }
}
