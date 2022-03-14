using System.Diagnostics.CodeAnalysis;

#nullable disable
namespace BabyMemory.Infrastructure.Data.Models
{
    using BabyMemory.Infrastructure.Shared;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Identity;

    public class User : IdentityUser
    {
        [MaxLength(GlobalConstants.UserFullNameMAxLenDb)]
        [Required]
        public string UserFullName { get; set; }

        [MaxLength(GlobalConstants.UrlMaxLen)]
        [AllowNull]
        public string Picture { get; set; }

        public DateTime RegisterDate { get; set; }

        public ICollection<Children> Childrens { get; set; } = new List<Children>();

        public ICollection<Event> Events { get; set; } = new List<Event>();
    }
}
