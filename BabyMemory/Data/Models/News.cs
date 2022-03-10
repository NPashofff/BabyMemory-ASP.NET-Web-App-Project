namespace BabyMemory.Data.Models
{
    using SharedTrip.Shared;
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
        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
