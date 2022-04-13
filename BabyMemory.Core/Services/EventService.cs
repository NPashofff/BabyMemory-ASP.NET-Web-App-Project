
using BabyMemory.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BabyMemory.Core.Services
{
    using Infrastructure.Data;
    using Infrastructure.Models;
    using Contracts;

    public class EventService : IEventService
    {
        private readonly ApplicationDbContext _repo;
        private readonly IUserService _userService;

        public EventService(ApplicationDbContext repo, IUserService userService)
        {
            _repo = repo;
            _userService = userService;
        }

        public async Task<ICollection<EventViewModel>> GetAllActiveEventsAsync()
        {
            return await _repo.Events
                .Where(x => x.IsPublic == true /*&& x.EventDate <= DateTime.Now*/)
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

        public async Task CreateEventAsync(EventViewModel model, string userName)
        {
            var user = await _userService.GetUserAsync(userName);
            Event newEvent = new Event
            {
                Name = model.Name,
                Description = model.Description,
                CreationDate = model.CreationDate,
                EventDate = model.EventDate,
                IsPublic = model.IsPublic
            };
            user.Events.Add(newEvent);
            _repo.Users.Update(user);

            await _repo.SaveChangesAsync();
        }
    }
}
