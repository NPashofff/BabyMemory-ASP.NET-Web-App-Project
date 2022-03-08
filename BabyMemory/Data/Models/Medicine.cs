namespace BabyMemory.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using SharedTrip.Shared;

    public class Medicine
    {
        [Key]
        [MaxLength(GlobalConstants.IdGuidMaxLen)]
        public string Id { get; set; } = new Guid().ToString();

        [Required]
        [MaxLength(GlobalConstants.HealthProcedureNameMAxLenDb)]
        public string Name { get; set; }
    }
}
