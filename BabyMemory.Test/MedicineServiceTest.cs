namespace BabyMemory.Test
{
    using Core.Contracts;
    using Core.Services;
    using Infrastructure.Data.Models;
    using Infrastructure.Data.Repositories;
    using Infrastructure.Models;
    using Microsoft.Extensions.DependencyInjection;
    using NUnit.Framework;
    using System.Linq;
    using System.Threading.Tasks;

    public class MedicineServiceTest
    {
        private ServiceProvider serviceProvider;
        private InMemoryDbContext dbContext;

        [SetUp]
        public Task Setup()
        {
            dbContext = new InMemoryDbContext();
            var serviceCollection = new ServiceCollection();

            serviceProvider = serviceCollection
                .AddSingleton(sp => dbContext.CreateContext())
                .AddSingleton<IApplicatioDbRepository, ApplicatioDbRepository>()
                .AddSingleton<IMedicineService, MedicineService>()
                .BuildServiceProvider();
            return Task.CompletedTask;
        }

        [Test]
        public async Task GetAllUsersTest()
        {
            var repo = serviceProvider.GetService<IApplicatioDbRepository>();
            if (repo == null) return;

            var medicineService = serviceProvider.GetService<IMedicineService>();
            if (medicineService == null) return;

            MedicineAddViewModel medicine = new()
            {
                Name = "NOTnull",
                Description = "llllllllll"
            };

            await medicineService.AddMedicineAsync(medicine);

            Assert.That(repo.All<Medicine>().Count(), Is.EqualTo(1));

        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }
    }
}
