﻿using BabyMemory.Infrastructure.Models;

namespace BabyMemory.Core.Contracts
{
    public interface INewsService
    {
        Task<NewsViewModel[]> GetAllNews();
        
        (bool, string) AddNews(AddNewsViewModel model);
        
        (bool, string) DeleteNews(string id);
    }
}
