namespace BabyMemory.Infrastructure.Models
{
    using System.ComponentModel.DataAnnotations;
    using Shared;

    public class NewsNewVewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public ICollection<string> Description { get; set; }

        [Display(Name = GlobalConstants.ImageName)]
        public string Picture { get; set; } =
            "https://static.tildacdn.com/tild6437-3735-4635-a539-346232383864/News.jpg";

        public bool IsActive { get; set; } = true;
    }
}
