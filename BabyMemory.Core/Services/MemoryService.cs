using BabyMemory.Core.Contracts;
using BabyMemory.Infrastructure.Data;
using BabyMemory.Infrastructure.Data.Models;
using BabyMemory.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace BabyMemory.Core.Services
{
    public class MemoryService : IMemoryService
    {
        private readonly ApplicationDbContext _repo;
        public MemoryService(ApplicationDbContext repo)
        {
            _repo = repo;
        }


        public void AddMemory(MemoryAddViewModel model, User currentUser)
        {
            Memory memory = new Memory
            {
                CreationDate = model.CreationDate,
                Name = model.Name,
                Description = model.Description,
                Picture = model.Picture
            };

            var user = _repo.Users
                .Include(x=>x.Childrens)
                .FirstOrDefault(x => x.Id == currentUser.Id);

            var child = user.Childrens.FirstOrDefault(x => x.Id == model.ChildId);

            child.Memories.Add(memory);

            _repo.SaveChanges();
        }
    }
}
