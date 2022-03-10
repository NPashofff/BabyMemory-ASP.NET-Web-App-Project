using System.Security.Principal;

namespace BabyMemory.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using SharedTrip.Shared;

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
        public string? Description { get; set; }

        [MaxLength(GlobalConstants.UrlMaxLen)]
        public string? Picture { get; set; }
    }
}
