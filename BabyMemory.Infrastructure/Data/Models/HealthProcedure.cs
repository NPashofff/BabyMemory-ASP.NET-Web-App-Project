using System.Diagnostics.CodeAnalysis;

#nullable disable
namespace BabyMemory.Infrastructure.Data.Models
{
    using Shared;
    using System.ComponentModel.DataAnnotations;

    public class HealthProcedure
    {
        [Key]
        [MaxLength(GlobalConstants.IdGuidMaxLen)]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(GlobalConstants.HealthProcedureNameMAxLenDb)]
        public string Name { get; set; }

        [MaxLength(GlobalConstants.MemoryDescriptionMaxLen)]
        [AllowNull]
        public string Description { get; set; }

        public DateTime CreationDate { get; set; }

        public ICollection<Medicine> Medicines { get; set; } = new List<Medicine>();
    }
}
