using BabyMemory.Core.Contracts;
using BabyMemory.Infrastructure.Data;
using BabyMemory.Infrastructure.Data.Models;
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

        public (bool, string) AddNews(AddNewsViewModel model)
        {
            var news = new News
            {
                Name = model.Name,
                Description = model.Description
            };

            _context.News.Add(news);
            var result =_context.SaveChanges();

            if (result == 1)
            {
                return (true, "");
            }
            else
            {
                return (false, GlobalConstants.NewsAddError);
            }
        }

        public (bool, string) DeleteNews(string id)
        {
            var news = _context.News.FirstOrDefault(x => x.Id == id);
            _context.News.Remove(news);
            var result = _context.SaveChanges();

            if (result == 1)
            {
                return (true, "");
            }
            else
            {
                return (false, GlobalConstants.ChidenNotDeletedError);
            }
        }
    }
}
