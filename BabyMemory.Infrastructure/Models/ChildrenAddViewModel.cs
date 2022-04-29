#nullable disable
namespace BabyMemory.Infrastructure.Models
{
    using BabyMemory.Infrastructure.Shared;
    using System.ComponentModel.DataAnnotations;
    public class ChildrenAddViewModel
    {
        [Required]
        [MaxLength(GlobalConstants.UserNameMaxLenDb)]
        public string Name { get; set; }

        [Required]
        [MaxLength(GlobalConstants.UserNameMaxLenDb)]
        public string LastName { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }


        [MaxLength(GlobalConstants.UrlMaxLen)]
        public string Picture { get; set; } = GlobalConstants.DefaultPicture;
    }
}
