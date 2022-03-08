using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NuGet.Protocol.Core.Types;
using SharedTrip.Shared;

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

        public User User { get; set; }

        //public ICollection<string> ChildrenId { get; set; }
    }
}
