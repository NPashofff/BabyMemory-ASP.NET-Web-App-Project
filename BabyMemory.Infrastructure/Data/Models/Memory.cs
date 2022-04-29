using System.Diagnostics.CodeAnalysis;

#nullable disable
namespace BabyMemory.Infrastructure.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using BabyMemory.Infrastructure.Shared;

    public class Memory
    {
        [Key]
        [MaxLength(GlobalConstants.IdGuidMaxLen)]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public DateTime CreationDate { get; set; }

        [Required]
        [MaxLength(GlobalConstants.MemoryNameMaxLen)]
        public string Name { get; set; }

        [MaxLength(GlobalConstants.MemoryDescriptionMaxLen)]
        [AllowNull]
        public string Description { get; set; }

        [MaxLength(GlobalConstants.UrlMaxLen)]
        [AllowNull]
        [Display(Name = GlobalConstants.ImageName)]
        public string Picture { get; set; }
    }
}
