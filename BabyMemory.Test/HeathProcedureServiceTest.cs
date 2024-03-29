﻿namespace BabyMemory.Test
{
    using Core.Contracts;
    using Core.Services;
    using Infrastructure.Data.Models;
    using Infrastructure.Data.Repositories;
    using Infrastructure.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    
    public class HeathProcedureServiceTest
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
                .AddSingleton<IHealthProcedureService, HealthProcedureService>()
                .BuildServiceProvider();

            var repo = serviceProvider.GetService<IApplicatioDbRepository>();
            if (repo != null) await SeedDbAsync(repo);
        }

        [Test]
        public async Task GetAllUsersTest()
        {
            var healthProcedureService = serviceProvider.GetService<IHealthProcedureService>();
            if (healthProcedureService != null)
            {
                var result = await healthProcedureService.GetAllMedicinesAsync();
                Assert.That(result.Count, Is.EqualTo(1));
            }
        }

        [Test]
        public async Task AddHeathProceduseTest()
        {
            var repo = serviceProvider.GetService<IApplicatioDbRepository>();
            var healthProcedureService = serviceProvider.GetService<IHealthProcedureService>();
            if (repo != null)
            {
                var medicines = await repo.All<Medicine>()
                    .Select(x => x.Id).ToListAsync();
                HealthProcedureViewModel healthProcedure = new()
                {
                    Name = "Imeto e",
                    Description = "null",
                    CreationDate = DateTime.Now,
                    Medicines = medicines
                };
                var child = repo.All<User>().First().Childrens.First();
                if (healthProcedureService != null)
                    await healthProcedureService.AddHealthProcedureAsync(healthProcedure, child.Id);


                Assert.That(child.HealthProcedures.Count, Is.EqualTo(1));
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

            Medicine medicine = new()
            {
                Name = "null",
                Description = "null",
                HealthProcedures = new List<HealthProcedure>()
            };

            await repo.AddAsync(medicine);
            await repo.AddAsync(user);
            await repo.SaveChangesAsync();
        }
    }
}
