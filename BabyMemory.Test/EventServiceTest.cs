using System;
using System.Linq;
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
    public class EventServiceTest
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
                .AddSingleton<IEventService, EventService>()
                .AddSingleton<IUserService, UserService>()
                .BuildServiceProvider();

            var repo = serviceProvider.GetService<IApplicatioDbRepository>();
            if (repo != null) await SeedDbAsync(repo);
        }

        [Test]
        public async Task GetAllUsersTest()
        {
            var eventService = serviceProvider.GetService<IEventService>();

            Assert.IsNotNull(eventService);
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
