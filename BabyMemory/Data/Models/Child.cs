namespace BabyMemory.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using SharedTrip.Shared;

    public class Child
    {
        [Key]
        [MaxLength(GlobalConstants.IdGuidMaxLen)]
        public string Id { get; set; } = new Guid().ToString();

        [Required]
        [MaxLength(GlobalConstants.UserNameMaxLenDb)]
        public string Name { get; set; }

        [Required]
        [MaxLength(GlobalConstants.UserNameMaxLenDb)]
        public string LastName { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [MaxLength(GlobalConstants.UrlMaxLen)]
        public string? Picture { get; set; }

        public ICollection<Memory> Memories { get; set; }

        public ICollection<Event> Events { get; set; }

        public ICollection<HealthProcedure> HelthProcedures { get; set; }
    }
}
