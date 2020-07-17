using System.ComponentModel.DataAnnotations;

namespace PPETracker.Models
{
    public class Recipient
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        public bool IsActive { get; set; }
    }
}
