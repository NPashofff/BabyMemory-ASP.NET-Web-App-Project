namespace BabyMemory.Infrastructure.Models
{
    using System.ComponentModel.DataAnnotations;
    using Shared;

    public class MemoryAddViewModel
    {
        public string? ChildId { get; set; }

        [Required]
        [StringLength(GlobalConstants.MemoryNameMaxLen,
            MinimumLength = GlobalConstants.MemoryNameMinLen)]
        public string Name { get; set; }
        
        public string? Description { get; set; }

        public DateTime CreationDate { get; set; }
        
        public string? Picture { get; set; }
    }
}
