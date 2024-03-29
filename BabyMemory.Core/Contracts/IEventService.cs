﻿using BabyMemory.Infrastructure.Data.Models;
using BabyMemory.Infrastructure.Models;

namespace BabyMemory.Core.Contracts
{
    public interface IEventService
    {
        Task<ICollection<EventViewModel>> GetAllActiveEventsAsync();
        
        Task CreateEventAsync(EventViewModel model, string userName);
        
        Task<ICollection<EventViewModel>> GetMyEventsAsync(string? identityName);
        
        Task<EventViewModel?> GetEventByIdAsync(string eventId);
        
        Task EditEventAsync(EventViewModel model);
        
        Task DeleteEventAsync(string eventId);
        
        Task<ICollection<EventViewModel>> GetAllEventsAsync();
        Task<Event?> GetEventAsync(string eventId);
    }
}
