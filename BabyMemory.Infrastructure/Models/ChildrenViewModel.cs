#nullable disable
namespace BabyMemory.Infrastructure.Models
{
    using System.ComponentModel.DataAnnotations;
    using BabyMemory.Infrastructure.Data.Models;
    using Shared;

    public class ChildrenViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        [Display(Name = GlobalConstants.ImageName)]
        public string Picture { get; set; }

        public ICollection<Memory> Memories { get; set; } = new List<Memory>();

        public ICollection<HealthProcedure> HealthProcedures { get; set; } = new List<HealthProcedure>();
    }
}
