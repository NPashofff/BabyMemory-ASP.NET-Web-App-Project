using System.Diagnostics.CodeAnalysis;

#nullable disable
namespace BabyMemory.Infrastructure.Data.Models
{
    using BabyMemory.Infrastructure.Shared;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Event
    {
        [Key]
        [MaxLength(GlobalConstants.IdGuidMaxLen)]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(GlobalConstants.EventNameMaxLen)]
        public string Name { get; set; }

        [MaxLength(GlobalConstants.EventDescriptionMaxLen)]
        [AllowNull]
        public string Description { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime EventDate { get; set; }

        [Required]
        public bool IsPublic { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        public User User { get; set; }

        public ICollection<Children> Childrens { get; set; } = new List<Children>();

    }
}
