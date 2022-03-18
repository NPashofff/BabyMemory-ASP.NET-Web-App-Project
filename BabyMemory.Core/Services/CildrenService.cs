using BabyMemory.Infrastructure.Shared;

#nullable disable
namespace BabyMemory.Core.Services
{
    using Contracts;
    using BabyMemory.Infrastructure.Data;
    using BabyMemory.Infrastructure.Data.Models;
    using Infrastructure.Models;
    using Microsoft.EntityFrameworkCore;

    public class ChildrenService : IChildrenService
    {
        private readonly ApplicationDbContext _repo;

        public ChildrenService(ApplicationDbContext repo)
        {
            _repo = repo;
        }

        public void AddChildren(ChildrenAddViewModel model, string userName)
        {
            var children = new Children
            {
                Name = model.Name,
                LastName = model.LastName,
                BirthDate = model.BirthDate,
                Picture = model.Picture,
                Memories = new List<Memory>(),
                HealthProcedures = new List<HealthProcedure>()
            };
            var user = _repo.Users.FirstOrDefault(x => x.UserName == userName);
            user?.Childrens.Add(children);

            _repo.SaveChanges();
        }



        public ChildrenViewModel[] All(string name)
        {
            var user = _repo.Users
                .Include(x => x.Childrens)
                .ThenInclude(c =>c.Memories)
                .FirstOrDefault(x => x.UserName == name);

            var children = user.Childrens.Select(x => new ChildrenViewModel
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

        public (bool, string) Delete(string id)
        {
            Children children = _repo.Childrens.FirstOrDefault(x => x.Id == id);
            if (children != null)
            {
                _repo.Childrens.Remove(children);
            }

            var result = _repo.SaveChanges();

            if (result == 1)
            {
                return (true, "");
            }
            else
            {
                return (false, GlobalConstants.ChidenNotDeletedError);
            }
        }

        public ChildrenViewModel GetChildren(string id)
        {
            var children = _repo.Childrens
                .FirstOrDefault(x => x.Id == id);

            if (children != null)
            {
                return new ChildrenViewModel
                {
                    Id = children.Id,
                    Name = children.Name,
                    Age = GetAge(children.BirthDate),
                    LastName = children.LastName,
                    BirthDate = children.BirthDate,
                    Picture = children.Picture
                };
            }
            else
            {
                return new ChildrenViewModel();
            }
        }

        private static int GetAge(DateTime argBirthDate)
        {
            return (DateTime.Now - argBirthDate).Days / 365;
        }
    }
}
