using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PPETracker.ViewModels
{
    public class EditShipmentCommand
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Scheduled Ship Date")]
        public DateTime ScheduledShipDate { get; set; }

        [Required]
        [Display(Name = "Recipient")]
        public int RecipientID { get; set; }

        [StringLength(100)]
        [Display(Name = "Comments")]
        public string Comments { get; set; }

        public string UserName { get; set; }

        public List<ProductSummaryViewModel> AvailableProductList { get; set; }
        public List<ProductSelectionItem> ProductSelection { get; set; }
        public List<SelectListItem> RecipientSelectionList { get; set; }
        public List<SelectListItem> CategoryList { get; set; }
    }
}
