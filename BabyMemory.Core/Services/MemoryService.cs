#nullable disable
namespace BabyMemory.Core.Services
{
    using Contracts;
    using Infrastructure.Data;
    using Infrastructure.Data.Models;
    using Infrastructure.Models;
    using Microsoft.EntityFrameworkCore;

    public class MemoryService : IMemoryService
    {
        private readonly ApplicationDbContext _repo;
        public MemoryService(ApplicationDbContext repo)
        {
            _repo = repo;
        }


        public async Task AddMemory(MemoryAddViewModel model, User currentUser)
        {
            Memory memory = new Memory
            {
                CreationDate = model.CreationDate,
                Name = model.Name,
                Description = model.Description,
                Picture = model.Picture
            };

            var user = await _repo.Users
                .Include(x=>x.Childrens)
                .FirstOrDefaultAsync(x => x.Id == currentUser.Id);

            var child = user.Childrens.FirstOrDefault(x => x.Id == model.ChildId);

            child.Memories.Add(memory);

            await _repo.SaveChangesAsync();
        }

        public async Task<Memory> GetMemoryAsync(string id)
        {
            return await _repo.Memories.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Edit(Memory model)
        {
            _repo.Memories.Update(model);
            await _repo.SaveChangesAsync();
        }

        public async Task DeleteAsync(Memory model)
        {
            _repo.Memories.Remove(model);
           await _repo.SaveChangesAsync();
        }
    }
}
