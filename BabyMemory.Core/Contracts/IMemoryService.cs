namespace BabyMemory.Core.Contracts
{
    using Infrastructure.Data.Models;
    using Infrastructure.Models;

    public interface IMemoryService
    {
        void AddMemory(MemoryAddViewModel model, User currentUser);

        Task<Memory> GetMemoryAsync(string id);

        Task Edit(Memory model);

        Task DeleteAsync(Memory model);
    }
}
