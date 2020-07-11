using System.ComponentModel.DataAnnotations;

namespace PPETracker.Models
{
    public class Mask : Product
    {
        [StringLength(50, MinimumLength = 3)]
        public string MaskType { get; set; }
    }
}
