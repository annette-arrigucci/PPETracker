using System.ComponentModel.DataAnnotations;

namespace PPETracker.Models
{
    public class HandSanitizer : Product
    {
        public int NumOunces { get; set; }

        [StringLength(30, MinimumLength = 3)]
        public string SanitizerType { get; set; }
    }
}
