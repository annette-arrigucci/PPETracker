using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PPETracker.ViewModels
{
    public class CreateProductCommand
    {
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [DisplayName("Category")]
        public int CategoryID { get; set; }

        //add dropdown data for category
        [DisplayName("Category")]
        public List<SelectListItem> CategoryOptions { get; set; }

        [DisplayName("Image File")]
        public string PhotoLink { get; set; }

        //image file
        [DisplayName("Select a File (optional)")]
        public IFormFile File { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Brand { get; set; }

        [DisplayName("Comments (optional)")]
        [StringLength(100)]
        public string Comments { get; set; }

        [DisplayName("Canister Type")]
        public string CanisterType { get; set; }

        [DisplayName("Canister Type")]
        public List<SelectListItem> CanisterTypeOptions { get; set; }

        [DisplayName("Gas Mask Type")]
        public string GasMaskType { get; set; }

        [DisplayName("Gas Mask Type")]
        public List<SelectListItem> GasMaskTypeOptions { get; set; }

        [DisplayName("Gas mask this canister is associated with")]
        public string GasMaskAssociatedWith { get; set; }

        [DisplayName("Gas mask this canister is associated with")]
        public List<SelectListItem> GasMaskAssociatedWithOptions { get; set; }

        [StringLength(30, MinimumLength = 3)]
        [DisplayName("Enter Gas Mask name")]
        public string UserEnteredGasMaskAssociatedWith { get; set; }

        [DisplayName("Glove Quantity")]
        [Range(0, 1000)]
        public int? GloveQuantity { get; set; }

        [DisplayName("Glove Size")]
        public string GloveSize { get; set; }

        [DisplayName("Glove Size")]
        public List<SelectListItem> GloveSizeOptions { get; set; }

        [StringLength(30, MinimumLength = 1)]
        [DisplayName("Enter new Glove Size")]
        public string UserEnteredGloveSize { get; set; }

        [DisplayName("Glove thickness (mm) (optional)")]
        [Range(4, 14)]
        public int? GloveThickness { get; set; }

        [DisplayName("Goggles Type")]
        public string GogglesType { get; set; }

        [DisplayName("Goggles Type")]
        public List<SelectListItem> GoggleTypeOptions { get; set; }

        [DisplayName("Number of ounces")]
        [Range(0, 128)]
        public int? NumOunces { get; set; }

        [DisplayName("Sanitizer Type")]
        public string SanitizerType { get; set; }

        [DisplayName("Sanitizer Type")]
        public List<SelectListItem> SanitizerTypeOptions { get; set; }

        [DisplayName("Mask Type")]
        public string MaskType { get; set; }

        [StringLength(30, MinimumLength = 3)]
        [DisplayName("Enter new Mask Type")]
        public string UserEnteredMaskType { get; set; }
        public List<SelectListItem> MaskTypeOptions { get; set; }

        [DisplayName("Wipe Quantity")]
        [Range(0, 1000)]
        public int? WipeQuantity { get; set; }

    }
}
