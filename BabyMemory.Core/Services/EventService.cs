namespace BabyMemory.Core.Services
{
    using Infrastructure.Data.Models;
    using Microsoft.EntityFrameworkCore;
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
                .Where(x => x.IsPublic == true && x.EventDate >= DateTime.Now)
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

        public async Task<ICollection<EventViewModel>> GetMyEventsAsync(string? identityName)
        {
            var user = await _userService.GetUserAsync(identityName);
            var events = await _repo.Events
                .Where(x => x.UserId == user.Id)
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
            return events;
        }

        public async Task<EventViewModel?> GetEventByIdAsync(string eventId)
        {
            return await _repo.Events
                .Where(x => x.Id == eventId)
                .Select(e => new EventViewModel
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description,
                CreationDate = e.CreationDate,
                EventDate = e.EventDate,
                IsPublic = e.IsPublic
            }).FirstOrDefaultAsync();

        }

        public async Task EditEventAsync(Event model)
        {
            var eventToUpdate = await _repo.Events.FirstOrDefaultAsync(x => x.Id == model.Id);
            eventToUpdate.Name = model.Name;
            eventToUpdate.Description = model.Description;
            eventToUpdate.EventDate = model.EventDate;
            eventToUpdate.IsPublic = model.IsPublic;
            
            //_repo.Events.Update(model);
            await _repo.SaveChangesAsync();
        }

        public async Task DeleteEventAsync(string eventId)
        {
            var eventToDelete = await _repo.Events.FirstOrDefaultAsync(x => x.Id == eventId);
            _repo.Events.Remove(eventToDelete);
            await _repo.SaveChangesAsync();
        }

        
        public async Task<ICollection<EventViewModel>> GetAllEventsAsync()
        {
            var events = await _repo.Events.Select(x => new EventViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                CreationDate = x.CreationDate,
                EventDate = x.EventDate,
                IsPublic = x.IsPublic
            }).ToListAsync();

            return events;
        }
    }
}
