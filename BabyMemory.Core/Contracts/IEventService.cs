using BabyMemory.Infrastructure.Models;

namespace BabyMemory.Core.Contracts
{
    public interface IEventService
    {
        Task<ICollection<EventViewModel>> GetAllActiveEventsAsync();
    }
}
