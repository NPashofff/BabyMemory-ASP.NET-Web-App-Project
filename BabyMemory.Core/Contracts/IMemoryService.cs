using BabyMemory.Infrastructure.Data.Models;
using BabyMemory.Infrastructure.Models;

namespace BabyMemory.Core.Contracts
{
    public interface IMemoryService
    {
        void AddMemory(MemoryAddViewModel model, User currentUser);
    }
}
