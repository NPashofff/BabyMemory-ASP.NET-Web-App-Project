namespace BabyMemory.Test
{
    using Core.Contracts;
    using Core.Services;
    using Infrastructure.Data;
    using Infrastructure.Data.Models;
    using Infrastructure.Data.Repositories;
    using Microsoft.Extensions.DependencyInjection;
    using NUnit.Framework;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    
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
