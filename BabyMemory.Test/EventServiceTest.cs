namespace BabyMemory.Test
{
    using Core.Contracts;
    using Core.Services;
    using Infrastructure.Data.Models;
    using Infrastructure.Data.Repositories;
    using Infrastructure.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Moq;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

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
                .BuildServiceProvider();

            var repo = serviceProvider.GetService<IApplicatioDbRepository>();
            if (repo != null) await SeedDbAsync(repo);
        }

        [Test]
        public async Task GetAllActiveEventTest()
        {
            var repo = serviceProvider.GetService<IApplicatioDbRepository>();
            var result = await repo.All<Event>().ToListAsync();


            var eventService = new Mock<IEventService>();
            eventService.Setup(x => x.GetAllActiveEventsAsync())
                .Returns(GetAllActiveEventsAsync(repo));

            var result1 = await eventService.Object.GetAllActiveEventsAsync();

            Assert.IsNotNull(result1);
            Assert.AreEqual(1, result.Count());
        }

        [Test]
        public async Task GetMyAllActiveEventSecondTest()
        {
            var eventService = serviceProvider.GetService<IEventService>();

            var result = await eventService.GetMyEventsAsync("pesho");

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
        }

        [Test]
        public async Task CreateEventAsyncTest()
        {
            var repo = serviceProvider.GetService<IApplicatioDbRepository>();
            var result = await repo.All<Event>().ToListAsync();

            var userName = "pesho";
            var model = new EventViewModel
            {
                Id = "null",
                Name = "null",
                Description = "DateTime.Now",
                CreationDate = DateTime.Now,
                EventDate = DateTime.Now.AddDays(1),
                IsPublic = true
            };

            var eventService = new Mock<IEventService>();
            eventService.Setup(x => x.CreateEventAsync(model, userName))
                .Returns(CreateEventAsync(model, userName));

            await eventService.Object.CreateEventAsync(model, userName);
            var result1 = await repo.All<Event>().ToArrayAsync();

            Assert.IsNotNull(result1);
            Assert.AreEqual(2, result1.Count());
        }

        [Test]
        public async Task CreateEventAsyncSecondTest()
        {
            var repo = serviceProvider.GetService<IApplicatioDbRepository>();
            var eventService = serviceProvider.GetService<IEventService>();

            var userName = "pesho";
            var model = new EventViewModel
            {
                Id = "null",
                Name = "null",
                Description = "DateTime.Now",
                CreationDate = DateTime.Now,
                EventDate = DateTime.Now.AddDays(1),
                IsPublic = true
            };

            await eventService.CreateEventAsync(model, userName);
            var result = await repo.All<Event>().ToArrayAsync();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public async Task GetEventByIdTest()
        {
            var eventService = serviceProvider.GetService<IEventService>();

            var result = await eventService.GetEventByIdAsync("1234");

            Assert.IsNotNull(result);
            Assert.That(result.Name == "Event1");
        }

        [Test]
        public async Task GetAllActiveEventSecondTest()
        {
            var eventService = serviceProvider.GetService<IEventService>();

            var result = await eventService.GetAllActiveEventsAsync();

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
        }

        [Test]
        public async Task EditEventTest()
        {
            var eventService = serviceProvider.GetService<IEventService>();
            var repo = serviceProvider.GetService<IApplicatioDbRepository>();
            var eventEdited = repo.All<Event>().First();
            eventEdited.Name = "gosho";

            await eventService.EditEventAsync(eventEdited);

            var result = repo.All<Event>().First();

            Assert.IsNotNull(result);
            Assert.AreEqual("gosho", result.Name);
        }

        [Test]
        public async Task DeleteEventTest()
        {
            var eventService = serviceProvider.GetService<IEventService>();

            eventService.DeleteEventAsync("1234");

            var result = await eventService.GetAllEventsAsync();

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }

        private async Task CreateEventAsync(EventViewModel model, string userName)
        {
            var repo = serviceProvider.GetService<IApplicatioDbRepository>();
            var user = repo.All<User>().First(x => x.UserName == userName);

            Event event1 = new Event()
            {
                Name = "null",
                Description = null,
                CreationDate = DateTime.Now,
                EventDate = DateTime.Now.AddDays(1),
                IsPublic = true,
            };

            user.Events.Add(event1);
            await repo.SaveChangesAsync();
        }


        private async Task<ICollection<EventViewModel>> GetAllActiveEventsAsync(IApplicatioDbRepository repo)
        {
            return await repo.All<Event>().Select(x => new EventViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                CreationDate = x.CreationDate,
                EventDate = x.EventDate,
                IsPublic = x.IsPublic
            }).ToListAsync();
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
                Id = "12345",
                UserName = "pesho",
                Email = "Pesho@pesho.com",
                UserFullName = "null",

            };

            var event1 = new Event
            {
                Id = "1234",
                Name = "Event1",
                Description = "Event1",
                CreationDate = DateTime.Now,
                EventDate = DateTime.Now.AddDays(1),
                IsPublic = true,
                UserId = user.Id
            };

            await repo.AddAsync(event1);
            await repo.AddAsync(user);
            await repo.SaveChangesAsync();
        }
    }
}
