using BabyMemory.Core.Contracts;
using BabyMemory.Infrastructure.Data;
using BabyMemory.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BabyMemory.Core.Services
{
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
    }
}
