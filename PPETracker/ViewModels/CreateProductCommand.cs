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
        public string CanisterType { get; set; }
        public string GasMaskAssociatedWith { get; set; }
        public int? GloveQuantity { get; set; }
        public string GloveSize { get; set; }
        public int? GloveThickness { get; set; }
        public string GogglesType { get; set; }
        public int? NumOunces { get; set; }
        public string SanitizerType { get; set; }

        [DisplayName("Mask Type")]
        public string MaskType { get; set; }

        [StringLength(30, MinimumLength = 3)]
        [DisplayName("Enter new Mask Type")]
        public string UserEnteredMaskType { get; set; }
        public List<SelectListItem> MaskTypeOptions { get; set; }
        public int? WipeQuantity { get; set; }

    }
}
