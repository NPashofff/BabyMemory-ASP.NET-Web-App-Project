
using Microsoft.EntityFrameworkCore;

namespace BabyMemory.Core.Services
{
    using Infrastructure.Data;
    using Infrastructure.Models;
    using Contracts;

    public class EventService : IEventService
    {
        private readonly ApplicationDbContext _repo;

        public EventService(ApplicationDbContext repo)
        {
            _repo = repo;
        }

        public async Task<ICollection<EventViewModel>> GetAllActiveEventsAsync()
        {
            return await _repo.Events
                .Where(x => x.IsPublic == true && x.EventDate <= DateTime.Now)
                .Select(e => new EventViewModel
                {
                    Id = e.Id,
                    Name = e.Name,
                    Description = e.Description,
                    CreationDate = e.CreationDate,
                    EventDate = e.EventDate,
                    IsPublic = e.IsPublic
                })
                .ToListAsync();
        }
    }
}
