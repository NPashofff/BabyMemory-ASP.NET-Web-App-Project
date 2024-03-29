﻿using System.Security.Cryptography.X509Certificates;

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

        public EventService(ApplicationDbContext repo )
        {
            _repo = repo;
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
            var user = await GetUserAsync(userName);
            Event newEvent = new()
            {
                Name = model.Name,
                Description = model.Description,
                CreationDate = model.CreationDate,
                EventDate = model.EventDate,
                IsPublic = model.IsPublic
            };
            if (user != null)
            {
                user.Events.Add(newEvent);
                _repo.Users.Update(user);
            }

            await _repo.SaveChangesAsync();
        }



        public async Task<ICollection<EventViewModel>> GetMyEventsAsync(string identityName)
        {
            var user = await GetUserAsync(identityName);
            var events = await _repo.Events
                .Where(x => user != null && x.UserId == user.Id)
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

        public async Task EditEventAsync(EventViewModel model)
        {
            var eventToUpdate = await _repo.Events.FirstOrDefaultAsync(x => x.Id == model.Id);
            if (eventToUpdate != null)
            {
                eventToUpdate.Name = model.Name;
                eventToUpdate.Description = model.Description;
                eventToUpdate.EventDate = model.EventDate;
                eventToUpdate.IsPublic = model.IsPublic;
            }

            //_repo.Events.Update(model);
            await _repo.SaveChangesAsync();
        }

        public async Task DeleteEventAsync(string eventId)
        {
            var eventToDelete = await _repo.Events.FirstOrDefaultAsync(x => x.Id == eventId);
            if (eventToDelete != null) _repo.Events.Remove(eventToDelete);
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

        public async Task<Event> GetEventAsync(string eventId)
        {
            return await _repo.Events.FirstOrDefaultAsync(x => x.Id == eventId);
        }

        private async Task<User> GetUserAsync(string userName)
        {
            return await _repo.Users.FirstOrDefaultAsync(x => x.UserName == userName);
        }
    }
}
