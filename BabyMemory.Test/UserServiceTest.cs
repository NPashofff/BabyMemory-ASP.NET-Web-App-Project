using System;
using System.Threading.Tasks;
using BabyMemory.Core.Contracts;
using BabyMemory.Core.Services;
using BabyMemory.Infrastructure.Data;
using BabyMemory.Infrastructure.Data.Models;
using BabyMemory.Infrastructure.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace BabyMemory.Test
{
    public class Tests
    {
        private ServiceProvider serviceProvider;
        private InMemoryDbContext dbContext;
        private IUserService _userService;

        [SetUp]
        public async Task Setup()
        {
            dbContext = new InMemoryDbContext();
            var serviceCollection = new ServiceCollection();

            serviceProvider = serviceCollection
                .AddSingleton(sp => dbContext.CreateContext())
                .AddSingleton<ApplicationDbContext>()
                .AddSingleton<IUserService, UserService>()
                .BuildServiceProvider();

            var repo = serviceProvider.GetService<ApplicationDbContext>();
            await SeedDbAsync(repo);
        }

        [Test]
        public void SetUserName()
        {
            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "pesho",
                Email = "Pesho@pesho.com",
                UserFullName = null,
            };

            var service = _userService.SetUserFullNameAsync(user, "Prsho Peshev");

            Assert.That(user.UserFullName, Is.EqualTo("Prsho Peshev"));
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
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
                UserFullName = null,
            };

            await repo.AddAsync(user);
            await repo.SaveChangesAsync();
        }
    }
}