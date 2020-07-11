using System.ComponentModel.DataAnnotations;

namespace PPETracker.Models
{
    public class GasMask : Product
    {
        [StringLength(50, MinimumLength = 3)]
        public string GasMaskType { get; set; }
    }
}
