using System;
using System.Linq;
using System.Threading.Tasks;
using BabyMemory.Core.Contracts;
using BabyMemory.Core.Services;
using BabyMemory.Infrastructure.Data;
using BabyMemory.Infrastructure.Data.Models;
using BabyMemory.Infrastructure.Data.Repositories;
using BabyMemory.Infrastructure.Models;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace BabyMemory.Test
{
    public class AdminServiceTest
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
                .AddSingleton<IApplicatioDbRepository, ApplicatioDbRepository>()
                //.AddSingleton<ApplicationDbContext>()
                .AddSingleton<IAdminService, AdminService>()
                .BuildServiceProvider();

            var repo = serviceProvider.GetService<IApplicatioDbRepository>();
            await SeedDbAsync(repo);
        }

        [Test]
        public async Task GetAllUsersTest()
        {
            var adminService = serviceProvider.GetService<IAdminService>();
            var users = await adminService.GetAllUsersAsync();

            Assert.That(users.Count, Is.EqualTo(1));
        }



        [Test]
        public async Task EditUserTest()
        {
            var adminService = serviceProvider.GetService<IAdminService>();
            var repo = serviceProvider.GetService<ApplicationDbContext>();
            var user = repo.Users.First();
            user.UserName = "Gosho";
            await adminService.EditUserAsync(user);

            Assert.That(repo.Users.First().UserName, Is.EqualTo("Gosho"));
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }

        private async Task SeedDbAsync(IApplicatioDbRepository repo)
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
