using System.ComponentModel.DataAnnotations;

namespace PPETracker.Models
{
    public class Goggles : Product
    {
        [StringLength(50, MinimumLength = 3)]
        public string GogglesType { get; set; }
    }
}
