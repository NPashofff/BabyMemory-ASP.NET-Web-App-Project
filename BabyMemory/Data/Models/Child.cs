using System.ComponentModel.DataAnnotations;

namespace BabyMemory.Data.Models
{
    public class Child
    {
        public string Id { get; set; } = new Guid().ToString();

        [MaxLength(254)]
        public string Name { get; set; }
    }
}
