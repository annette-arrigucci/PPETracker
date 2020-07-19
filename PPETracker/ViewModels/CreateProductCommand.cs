using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace PPETracker.ViewModels
{
    public class CreateProductCommand
    {
        public string Name { get; set; }
        [DisplayName("Category")]
        public int CategoryID { get; set; }
        //add dropdown data for category
        [DisplayName("Category")]
        public List<SelectListItem> CategoryOptions { get; set; }
        [DisplayName("Image File")]
        public string PhotoLink { get; set; }
        //image file
        [DisplayName("Select a File")]
        public IFormFile File { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
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
        [DisplayName("Enter new Mask Type")]
        public string UserEnteredMaskType { get; set; }
        public List<SelectListItem> MaskTypeOptions { get; set; }
        public int? WipeQuantity { get; set; }

    }
}
