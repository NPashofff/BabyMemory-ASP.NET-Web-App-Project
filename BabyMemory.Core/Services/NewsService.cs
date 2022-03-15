﻿using BabyMemory.Core.Contracts;
using BabyMemory.Infrastructure.Data;
using BabyMemory.Infrastructure.Models;
using BabyMemory.Infrastructure.Shared;

namespace BabyMemory.Core.Services
{
    public class NewsService : INewsService
    {
        private readonly ApplicationDbContext _context;

        public NewsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public NewsViewModel[] GetAllNews()
        {
           var result = _context.News
               .Where(x => x.IsActive == true)
               .OrderByDescending(x=>x.CreationDate)
                .Select(x => new NewsViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Picture = GlobalConstants.NewsLogo,
                    IsActive = x.IsActive
                }).ToArray();

           return result;
        }
    }
}