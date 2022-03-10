namespace BabyMemory.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using SharedTrip.Shared;

    public class Medicine
    {
        [Key]
        [MaxLength(GlobalConstants.IdGuidMaxLen)]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(GlobalConstants.MedicineNameMaxLen)]
        public string Name { get; set; }

        [MaxLength(GlobalConstants.MedicineDescriptionMaxLen)]
        public string? Description { get; set; }
    }
}
