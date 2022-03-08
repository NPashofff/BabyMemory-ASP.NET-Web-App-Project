namespace BabyMemory.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Identity;
    using SharedTrip.Shared;

    public class User : IdentityUser
    {
        [MaxLength(GlobalConstants.UserFullNameMAxLenDb)]
        [Required]
        public string UserFullName { get; set; }

        [MaxLength(GlobalConstants.UrlMaxLen)]
        public string? Picture { get; set; }

        public DateTime RegisterDate { get; set; }

        public ICollection<Children> Childrens { get; set; }

        public ICollection<Event> Events { get; set; }
    }
}
