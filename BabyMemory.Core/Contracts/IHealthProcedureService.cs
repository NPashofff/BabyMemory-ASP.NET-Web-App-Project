using BabyMemory.Infrastructure.Data.Models;

namespace BabyMemory.Core.Contracts
{
    public interface IHealthProcedureService
    {
        Task<List<Medicine>> GetAllMedicinesAsync();
        //List<Medicine> GetAllMedicinesAsync();
    }
}
