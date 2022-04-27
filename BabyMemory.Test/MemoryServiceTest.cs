namespace BabyMemory.Test
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Core.Contracts;
    using Core.Services;
    using Infrastructure.Data.Models;
    using Infrastructure.Data.Repositories;
    using Infrastructure.Models;
    using Microsoft.Extensions.DependencyInjection;
    using NUnit.Framework;

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
        public async Task AddMemoryToChildrenTest()
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

        [Test]
        public async Task GetMemoryTest()
        {
            var memoryService = serviceProvider.GetService<IMemoryService>();

            var result = await memoryService.GetMemoryAsync("12345");

            Assert.That(result.Id, Is.EqualTo("12345"));
        }

        [Test]
        public async Task EditMemoryTest()
        {
            var memoryService = serviceProvider.GetService<IMemoryService>();
            var repo = serviceProvider.GetService<IApplicatioDbRepository>();


            var memory = await memoryService.GetMemoryAsync("12345");
            memory.Name = "new name";
            await memoryService.Edit(memory);

            Assert.That(repo.All<Memory>().First().Name, Is.EqualTo(memory.Name));
        }

        [Test]
        public async Task DeleteMemoryTest()
        {
            var memoryService = serviceProvider.GetService<IMemoryService>();
            var repo = serviceProvider.GetService<IApplicatioDbRepository>();


            var memory = await memoryService.GetMemoryAsync("12345");
            await memoryService.DeleteAsync(memory);

            Assert.That(repo.All<Memory>().Count(), Is.EqualTo(0));
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
