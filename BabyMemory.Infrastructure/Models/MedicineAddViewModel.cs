namespace BabyMemory.Infrastructure.Models
{
    using System.ComponentModel.DataAnnotations;
    using Shared;

    public class MedicineAddViewModel
    {
        [Required]
        [StringLength(GlobalConstants.MedicineNameMaxLen,
            MinimumLength = GlobalConstants.MedicineNameMinLen)]
        public string Name { get; set; }

        public string? Description { get; set; }
    }
}
