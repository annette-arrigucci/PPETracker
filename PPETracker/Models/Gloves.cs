using System.ComponentModel.DataAnnotations;

namespace PPETracker.Models
{
    public class Gloves : Product
    {
        public int GloveQuantity { get; set; }

        [StringLength(10, MinimumLength = 3)]
        public string GloveSize { get; set; }

        public int GloveThickness { get; set; }
    }
}
