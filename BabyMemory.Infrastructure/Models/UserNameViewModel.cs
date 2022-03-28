using Microsoft.AspNetCore.Identity;

namespace BabyMemory.Infrastructure.Models
{
    public class UserNameViewModel
    {
        public string Id { get; set; }
        
        public string Username { get; set; }

        public List<string> Roles { get; set; }
    }
}
