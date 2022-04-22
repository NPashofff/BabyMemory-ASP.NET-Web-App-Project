namespace BabyMemory.Core.Contracts
{
    using BabyMemory.Infrastructure.Models;
    public interface IChildrenService
    {
        Task AddChildren(ChildrenAddViewModel model, string username);

        Task<ChildrenViewModel[]> All(string name);
        
        Task Delete(string id);
        
        Task<ChildrenViewModel> GetChildren(string id);
    }
}
