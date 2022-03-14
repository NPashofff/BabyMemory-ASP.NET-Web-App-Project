#nullable disable
namespace BabyMemory.Infrastructure.Models
{
    using BabyMemory.Infrastructure.Shared;
    using System.ComponentModel.DataAnnotations;
    public class AddNewsViewModel
    {
        [Required]
        [StringLength(GlobalConstants.NewsNameMaxLen)]
        public string Name { get; set; }

        [Required]
        [StringLength(GlobalConstants.NewsDescriptionMaxLen)]
        public string Description { get; set; }
    }
}
