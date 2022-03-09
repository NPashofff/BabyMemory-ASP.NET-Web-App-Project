namespace BabyMemory.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using SharedTrip.Shared;

    public class News
    {
        [Key]
        [MaxLength(GlobalConstants.IdGuidMaxLen)]
        public string Id { get; set; }

        [Required]
        [MaxLength(GlobalConstants.NewsNameMaxLen)]
        public string Name { get; set; }

        [MaxLength(GlobalConstants.NewsDescriptionMaxLen)]
        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
