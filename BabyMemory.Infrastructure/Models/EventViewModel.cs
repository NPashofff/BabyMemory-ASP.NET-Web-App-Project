using BabyMemory.Infrastructure.Shared;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace BabyMemory.Infrastructure.Models
{
    public class EventViewModel
    {
        [AllowNull]
        public string? Id { get; set; }

        [Required]
        [MaxLength(GlobalConstants.EventNameMaxLen)]
        public string Name { get; set; }

        [MaxLength(GlobalConstants.EventDescriptionMaxLen)]
        public string? Description { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime EventDate { get; set; }

        [Required]
        public bool IsPublic { get; set; }
    }
}
