using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PPETracker.ViewModels
{
    public class ProductDetailViewModel
    {
        [DisplayName("ID")]
        public int ID { get; set; }

        [DisplayName("Name")]
        public string Name { get; set; }

        [DisplayName("Category")]
        public int CategoryID { get; set; }

        [DisplayName("Category Name")]
        public string CategoryName { get; set; }

        [DisplayName("Image File")]
        public string PhotoLink { get; set; }

        [DisplayName("Brand")]
        public string Brand { get; set; }

        [DisplayName("Comments")]
        public string Comments { get; set; }

        [DisplayName("Canister Type")]
        public string CanisterType { get; set; }

        [DisplayName("Gas Mask Type")]
        public string GasMaskType { get; set; }

        [DisplayName("Gas mask this canister is associated with")]
        public string GasMaskAssociatedWith { get; set; }

        [DisplayName("Glove Quantity")]
        public int? GloveQuantity { get; set; }

        [DisplayName("Glove Size")]
        public string GloveSize { get; set; }

        [DisplayName("Glove thickness (mm)")]
        public int? GloveThickness { get; set; }

        [DisplayName("Goggles Type")]
        public string GogglesType { get; set; }

        [DisplayName("Number of ounces")]
        public int? NumOunces { get; set; }

        [DisplayName("Sanitizer Type")]
        public string SanitizerType { get; set; }

        [DisplayName("Mask Type")]
        public string MaskType { get; set; }

        [DisplayName("Wipe Quantity")]
        public int? WipeQuantity { get; set; }

        [DisplayName("Quantity")]
        [Range(0,10000)]
        public int Quantity { get; set; }
    }
}
