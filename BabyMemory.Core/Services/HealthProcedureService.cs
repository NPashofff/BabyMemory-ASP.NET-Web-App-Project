namespace BabyMemory.Core.Services
{
    using Contracts;
    using Infrastructure.Data;
    using Infrastructure.Data.Models;
    using Infrastructure.Models;
    using Microsoft.EntityFrameworkCore;
    public class HealthProcedureService : IHealthProcedureService
    {
        private readonly ApplicationDbContext _repo;

        public HealthProcedureService(ApplicationDbContext repo)
        {
            _repo = repo;
        }

        public async Task<List<Medicine>> GetAllMedicinesAsync()
        {
            return await _repo.Medicines.ToListAsync();
        }

        public async Task AddHealthProcedureAsync(HealthProcedureViewModel model, string childId)
        {
            List<Medicine> medicines = new();

            foreach (var variable in model.Medicines)
            {
                var medicine = await _repo.Medicines.FindAsync(variable);
                if (medicine != null) medicines.Add(medicine);
            }

            var procedure = new HealthProcedure
            {
                Name = model.Name,
                Description = model.Description,
                CreationDate = model.CreationDate,
                Medicines = medicines
            };

            var child = await _repo.Childrens.FindAsync(childId);
            if (child != null)
            {
                child.HealthProcedures.Add(procedure);

                _repo.Childrens.Update(child);
            }

            await _repo.SaveChangesAsync();
        }
    }
}
