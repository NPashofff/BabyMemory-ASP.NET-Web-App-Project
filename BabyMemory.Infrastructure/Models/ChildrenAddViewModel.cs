using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using BabyMemory.Infrastructure.Shared;

namespace BabyMemory.Infrastructure.Models
{
#nullable disable
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
        public string Picture { get; set; }
    }
}
