using BabyMemory.Infrastructure.Models;

namespace BabyMemory.Core.Contracts
{
    public interface IMedicineService
    {
        Task AddMedicineAsync(MedicineAddViewModel model);
    }
}
