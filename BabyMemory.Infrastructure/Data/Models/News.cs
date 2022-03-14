#nullable disable
namespace BabyMemory.Infrastructure.Data.Models
{
    using System.Diagnostics.CodeAnalysis;
    using BabyMemory.Infrastructure.Shared;
    using System.ComponentModel.DataAnnotations;

    public class News
    {
        [Key]
        [MaxLength(GlobalConstants.IdGuidMaxLen)]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(GlobalConstants.NewsNameMaxLen)]
        public string Name { get; set; }

        [MaxLength(GlobalConstants.NewsDescriptionMaxLen)]
        [AllowNull]
        public string Description { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now;

        public bool IsActive { get; set; } = true;
    }
}
