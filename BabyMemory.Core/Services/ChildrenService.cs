#nullable disable
namespace BabyMemory.Core.Services
{
    using Infrastructure.Shared;
    using Contracts;
    using Infrastructure.Data;
    using Infrastructure.Data.Models;
    using Infrastructure.Models;
    using Microsoft.EntityFrameworkCore;

    public class ChildrenService : IChildrenService
    {
        private readonly ApplicationDbContext _repo;

        public ChildrenService(ApplicationDbContext repo)
        {
            _repo = repo;
        }

        public async Task AddChildren(ChildrenAddViewModel model, string userName)
        {
            if (model.Picture == null) model.Picture = GlobalConstants.DefaultPicture;
            
            var children = new Children
            {
                Name = model.Name,
                LastName = model.LastName,
                BirthDate = model.BirthDate,
                Picture = model.Picture,
                Memories = new List<Memory>(),
                HealthProcedures = new List<HealthProcedure>()
            };
            var user = await _repo.Users.FirstOrDefaultAsync(x => x.UserName == userName);
            user?.Childrens.Add(children);

            await _repo.SaveChangesAsync();
        }

        public async Task<ChildrenViewModel[]> All(string name)
        {
            var user = await _repo.Users
                .Include(x => x.Childrens)
                .ThenInclude(c => c.Memories)
                .FirstOrDefaultAsync(x => x.UserName == name);

            var children = user
                .Childrens
                .Select(x => new ChildrenViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Age = GetAge(x.BirthDate),
                    LastName = x.LastName,
                    BirthDate = x.BirthDate,
                    Picture = x.Picture,
                    Memories = x.Memories
                }).ToArray();

            return children;
        }

        public async Task Delete(string id)
        {
            Children children = await _repo.Childrens.FirstOrDefaultAsync(x => x.Id == id);
            if (children != null)
            {
                _repo.Childrens.Remove(children);
            }

            var result = await _repo.SaveChangesAsync();
        }

        public async Task<ChildrenViewModel> GetChildren(string id)
        {
            var children =await _repo.Childrens
                .Include(x => x.Memories)
                .Include(x => x.HealthProcedures)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (children != null)
            {
                return new ChildrenViewModel
                {
                    Id = children.Id,
                    Name = children.Name,
                    Age = GetAge(children.BirthDate),
                    LastName = children.LastName,
                    BirthDate = children.BirthDate,
                    Picture = children.Picture,
                    Memories = children.Memories,
                    HealthProcedures = children.HealthProcedures
                };
            }
            else
            {
                return new ChildrenViewModel();
            }
        }

        public async Task Edit(ChildrenViewModel model)
        {
            var children =await _repo.Childrens.FirstOrDefaultAsync(x => x.Id == model.Id);
            
            if (children != null)
            {
                children.Name = model.Name;
                children.LastName = model.LastName;
                children.BirthDate = model.BirthDate;
                children.Picture = model.Picture;
            }

            await _repo.SaveChangesAsync();
        }

        private static int GetAge(DateTime argBirthDate)
        {
            return (DateTime.Now - argBirthDate).Days / 365;
        }
    }
}
