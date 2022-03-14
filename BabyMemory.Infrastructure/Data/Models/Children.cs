using System.Diagnostics.CodeAnalysis;

#nullable disable
namespace BabyMemory.Infrastructure.Data.Models
{
    using BabyMemory.Infrastructure.Shared;
    using System.ComponentModel.DataAnnotations;

    public class Children
    {
        [Key]
        [MaxLength(GlobalConstants.IdGuidMaxLen)]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(GlobalConstants.UserNameMaxLenDb)]
        public string Name { get; set; }

        [Required]
        [MaxLength(GlobalConstants.UserNameMaxLenDb)]
        public string LastName { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [MaxLength(GlobalConstants.UrlMaxLen)]
        [AllowNull]
        public string Picture { get; set; }

        public ICollection<Memory> Memories { get; set; } = new List<Memory>();

        public ICollection<HealthProcedure> HealthProcedures { get; set; } = new List<HealthProcedure>();
    }
}
