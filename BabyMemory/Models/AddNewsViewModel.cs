namespace BabyMemory.Models
{
    using System.ComponentModel.DataAnnotations;
    using SharedTrip.Shared;

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
