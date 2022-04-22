using BabyMemory.Core.Contracts;
using BabyMemory.Core.Services;
using BabyMemory.Infrastructure.Data;
using BabyMemory.Infrastructure.Data.Models;
using BabyMemory.Infrastructure.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;
using BabyMemory.Infrastructure.Models;

namespace BabyMemory.Test
{
    public class ChildrenServiceTest
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
                .AddSingleton<IChildrenService, ChildrenService>()
                .BuildServiceProvider();

            var repo = serviceProvider.GetService<IApplicatioDbRepository>();
            if (repo != null) await SeedDbAsync(repo);
        }

        [Test]
        public async Task GetAllUsersTest()
        {
            var childrenService = serviceProvider.GetService<IChildrenService>();
            var model = new ChildrenAddViewModel
            {
                Name = "peshoJR",
                LastName = "null",
                BirthDate = DateTime.Now,
                Picture = null
            };

            if (childrenService != null)
                await childrenService.AddChildren(model, "pesho");
            var repo = serviceProvider.GetService<IApplicatioDbRepository>();
            if (repo != null)
            {
                var user = repo.All<User>().First();
                Assert.That(user.Childrens.Count, Is.EqualTo(2));
            }
        }



        [Test]
        public async Task ChildrenAllTest()
        {
            var childrenService = serviceProvider.GetService<IChildrenService>();
            if (childrenService != null)
            {
                var result = await childrenService.All("pesho");

                Assert.That(result.Length, Is.EqualTo(1));
            }
        }

        [Test]
        public async Task ChildrenDeleteTest()
        {
            var childrenService = serviceProvider.GetService<IChildrenService>();
            var repo = serviceProvider.GetService<IApplicatioDbRepository>();
            if (repo != null)
            {
                var child = repo.All<User>().First().Childrens.First();
                if (childrenService != null) await childrenService.Delete(child.Id);

                Assert.That(repo.All<User>().First().Childrens.Count, Is.EqualTo(0));
            }
        }

        [Test]
        public async Task ChildrenGetChildrenTest()
        {
            var childrenService = serviceProvider.GetService<IChildrenService>();
            var repo = serviceProvider.GetService<IApplicatioDbRepository>();
            if (repo != null)
            {
                var children = repo.All<User>().First().Childrens.First();
                if (childrenService != null)
                {
                    var child = await childrenService.GetChildren(children.Id);

                    Assert.That(children.Name, Is.EqualTo(child.Name));
                }
            }
        }

        [Test]
        public async Task ChildrenGetAgeTest()
        {
            var childrenService = serviceProvider.GetService<IChildrenService>();
            var repo = serviceProvider.GetService<IApplicatioDbRepository>();
            if (repo != null)
            {
                var children = repo.All<User>().First().Childrens.First();
                if (childrenService != null)
                {
                    var age = await childrenService.GetChildren(children.Id);
                    Assert.That(age.Age, Is.EqualTo(0));
                }
            }
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
            Children child = new()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "pesho",
                LastName = "null",
                BirthDate = DateTime.Now,
                Picture = null,
            };
            user.Childrens.Add(child);
            await repo.AddAsync(user);
            await repo.SaveChangesAsync();
        }
    }
}
