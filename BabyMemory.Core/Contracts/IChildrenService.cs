namespace BabyMemory.Core.Contracts
{
    using BabyMemory.Infrastructure.Models;
    public interface IChildrenService
    {
        Task AddChildren(ChildrenAddViewModel model, string username);

        Task<ChildrenViewModel[]> All(string name);
        
        (bool, string) Delete(string id);
        
        ChildrenViewModel GetChildren(string id);
    }
}
