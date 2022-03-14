using BabyMemory.Infrastructure.Models;

namespace BabyMemory.Core.Contracts
{
    public interface INewsService
    {
        NewsViewModel[] GetAllNews();
    }
}
