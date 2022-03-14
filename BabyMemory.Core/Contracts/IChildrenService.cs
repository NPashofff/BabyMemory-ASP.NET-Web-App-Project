namespace BabyMemory.Core.Contracts
{
    using BabyMemory.Infrastructure.Models;
    public interface IChildrenService
    {
        void AddChildren(ChildrenAddViewModel model);
    }
}
