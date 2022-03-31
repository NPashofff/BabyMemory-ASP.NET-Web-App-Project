namespace BabyMemory.Infrastructure.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.CodeAnalysis;
    using Data.Models;
    using Shared;
    public class HealthProcedureViewModel
    {

        [Required]
        [StringLength(GlobalConstants.HealthProcedureNameMAxLenDb,
            MinimumLength = GlobalConstants.HealthProcedureNameMinLen)]
        public string Name { get; set; }

        [StringLength(GlobalConstants.MemoryDescriptionMaxLen,
            MinimumLength = GlobalConstants.MemoryDescriptionMinLen)]
        public string? Description { get; set; }

        public DateTime? CreationDate { get; set; } = DateTime.Now;

        public ICollection<string> Medicines { get; set; } = new List<string>();
    }
}
