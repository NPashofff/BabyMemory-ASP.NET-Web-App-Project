using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace BabyMemory.Data.Models
{
    public class User : IdentityUser
    {
        [MaxLength(254)]
        public string? UserFullName { get; set;}

        public ICollection<Child>? Children { get; set; }
    }
}
