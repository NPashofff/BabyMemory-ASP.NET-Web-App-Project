namespace BabyMemory.Core.Services
{
    using Contracts;
    using Infrastructure.Data;
    using Infrastructure.Data.Models;
    using Infrastructure.Models;
    using Infrastructure.Shared;
    using Microsoft.EntityFrameworkCore;
    
    public class NewsService : INewsService
    {
        private readonly ApplicationDbContext _context;

        public NewsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<NewsViewModel[]> GetAllNews()
        {
            var result = await _context.News
                .Where(x => x.IsActive == true)
                .OrderByDescending(x => x.CreationDate)
                 .Select(x => new NewsViewModel
                 {
                     Id = x.Id,
                     Name = x.Name,
                     Description = x.Description,
                     Picture = GlobalConstants.NewsLogo,
                     IsActive = x.IsActive
                 }).ToArrayAsync();

            return result;
        }

        //TODO Make ti Async
        public (bool, string) AddNews(AddNewsViewModel model)
        {
            var news = new News
            {
                Name = model.Name,
                Description = model.Description
            };

            _context.News.Add(news);
            var result = _context.SaveChanges();

            return result != 1 ? (false, GlobalConstants.NewsAddError) : (true, "");
        }

        public (bool, string) DeleteNews(string id)
        {
            var news = _context.News.FirstOrDefault(x => x.Id == id);
            if (news != null)
            {
                _context.News.Remove(news);
            }

            var result = _context.SaveChanges();

            return result == 1 ? (true, "") : (false, GlobalConstants.ChidenNotDeletedError);
        }
    }
}
