namespace BabyMemory.Core.Contracts
{
    using BabyMemory.Infrastructure.Models;
    public interface IChildrenService
    {
        void AddChildren(ChildrenAddViewModel model, string username);

        ChildrenViewModel[] All(string name);
        (bool, string) Delete(string id);
    }
}
