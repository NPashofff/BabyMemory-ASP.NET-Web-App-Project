using BabyMemory.Infrastructure.Data.Models;
using BabyMemory.Infrastructure.Models;

namespace BabyMemory.Core.Contracts
{
    public interface IHealthProcedureService
    {
        Task<List<Medicine>> GetAllMedicinesAsync();
        //List<Medicine> GetAllMedicinesAsync();
        
        Task AddHealthProcedureAsync(HealthProcedureViewModel model, string childId);
    }
}
