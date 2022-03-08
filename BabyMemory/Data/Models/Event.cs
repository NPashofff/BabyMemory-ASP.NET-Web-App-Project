using SharedTrip.Shared;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BabyMemory.Data.Models
{
    public class Event
    {
        [Key]
        [MaxLength(GlobalConstants.IdGuidMaxLen)]
        public string Id { get; set; } = new Guid().ToString();

        [Required]
        [MaxLength(GlobalConstants.EventNameMaxLen)]
        public string Name { get; set; }

        [MaxLength(GlobalConstants.EventDescriptionMaxLen)]
        public string? Description { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime EventDate { get; set; }

        [Required]
        public bool IsPublic { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        
    }
}
