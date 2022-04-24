using System;
using System.Linq;
using System.Threading.Tasks;
using BabyMemory.Core.Contracts;
using BabyMemory.Core.Services;
using BabyMemory.Infrastructure.Data.Models;
using BabyMemory.Infrastructure.Data.Repositories;
using BabyMemory.Infrastructure.Models;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace BabyMemory.Test
{
    public class MemoryServiceTest
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
                .AddSingleton<IMemoryService, MemoryService>()
                .BuildServiceProvider();

            var repo = serviceProvider.GetService<IApplicatioDbRepository>();
            if (repo != null) await SeedDbAsync(repo);
        }

        [Test]
        public async Task GetAllUsersTest()
        {
            var memoryService = serviceProvider.GetService<IMemoryService>();
            var repo = serviceProvider.GetService<IApplicatioDbRepository>();

            var user = repo?.All<User>().First();

            MemoryAddViewModel memoryAddViewModel = new()
            {
                ChildId = "1234",
                Name = "null vvv",
                Description = null,
                CreationDate = DateTime.Now,
                Picture = null
            };

            await memoryService.AddMemory(memoryAddViewModel, user);

            Assert.That(user.Childrens.First().Memories.Count, Is.EqualTo(2));
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
                Id = "123",
                UserName = "pesho",
                Email = "Pesho@pesho.com",
                UserFullName = "null",
            };

            Children child = new()
            {
                Id = "1234",
                Name = "peshoJR",
                LastName = "null",
                BirthDate = DateTime.Now,
                Picture = null,
            };

            Memory memory = new()
            {
                Id = "12345",
                CreationDate = DateTime.Now,
                Name = "No name",
                Description = "No description",
                Picture = "null",
            };

            child.Memories.Add(memory);
            user.Childrens.Add(child);

            await repo.AddAsync(user);
            await repo.SaveChangesAsync();
        }
    }
}
