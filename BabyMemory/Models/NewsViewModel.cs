namespace BabyMemory.Models
{
    public class NewsViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Picture { get; set; } =
            "https://static.tildacdn.com/tild6437-3735-4635-a539-346232383864/News.jpg";

        public bool IsActive { get; set; }
    }
}
