using System.ComponentModel.DataAnnotations;

namespace PPETracker.Models
{
    public class Canister : Product
    {
        [StringLength(50, MinimumLength = 3)]
        public string CanisterType { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string GasMaskAssociatedWith { get; set; }
    }
}
