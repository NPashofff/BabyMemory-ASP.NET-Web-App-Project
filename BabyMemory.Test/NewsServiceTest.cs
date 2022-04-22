namespace BabyMemory.Test
{
    using Core.Contracts;
    using Core.Services;
    using Infrastructure.Data;
    using Infrastructure.Data.Models;
    using Infrastructure.Data.Repositories;
    using Infrastructure.Models;
    using Microsoft.Extensions.DependencyInjection;
    using NUnit.Framework;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    
    public class NewsServiceTests
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
                .AddSingleton<INewsService, NewsService>()
                .BuildServiceProvider();

            var repo = serviceProvider.GetService<IApplicatioDbRepository>();
            if (repo != null) await SeedDbAsync(repo);
        }

        [Test]
        public void AddNewsTest()
        {
            var newsService = serviceProvider.GetService<INewsService>();
            var repo = serviceProvider.GetService<ApplicationDbContext>();
            if (repo == null || newsService == null) return;
            
            AddOneNews(repo, newsService);

            Assert.That(repo.News.Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task GetAllNewsTest()
        {
            var newsService = serviceProvider.GetService<INewsService>();
            var repo = serviceProvider.GetService<ApplicationDbContext>();
            if (repo == null || newsService == null) return;
            
            AddOneNews(repo, newsService);


            var allNewse = await newsService.GetAllNews();
            var count = allNewse.Count();

            Assert.That(count, Is.EqualTo(1));
        }

        [Test]
        public void RemoveNewsTest()
        {
            var newsService = serviceProvider.GetService<INewsService>();
            var repo = serviceProvider.GetService<ApplicationDbContext>();
            if (repo == null || newsService == null) return;
            
            AddOneNews(repo, newsService);
            string id = repo.News.First().Id;

            newsService.DeleteNews(id);

            Assert.That(repo.News.Count(), Is.EqualTo(0));
        }

        private void AddOneNews(ApplicationDbContext repo, INewsService newsService)
        {
            AddNewsViewModel news = new AddNewsViewModel()
            {
                Name = "Nivina",
                Description = " Description ",
            };
            newsService.AddNews(news); ;
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