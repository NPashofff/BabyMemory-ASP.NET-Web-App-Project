using System.Diagnostics.CodeAnalysis;

#nullable disable
namespace BabyMemory.Infrastructure.Data.Models
{
    using BabyMemory.Infrastructure.Shared;
    using System.ComponentModel.DataAnnotations;

    public class Medicine
    {
        [Key]
        [MaxLength(GlobalConstants.IdGuidMaxLen)]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(GlobalConstants.MedicineNameMaxLen)]
        public string Name { get; set; }

        [MaxLength(GlobalConstants.MedicineDescriptionMaxLen)]
        [AllowNull]
        public string Description { get; set; }
    }
}
