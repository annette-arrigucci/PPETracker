using System.ComponentModel.DataAnnotations;

namespace PPETracker.Models
{
    public class Category
    {
        public int ID { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Name { get; set; }
    }
}
