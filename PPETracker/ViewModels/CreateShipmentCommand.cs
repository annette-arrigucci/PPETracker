using Microsoft.AspNetCore.Mvc.Rendering;
using PPETracker.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PPETracker.ViewModels
{
    public class CreateShipmentCommand
    {
        [Required]
        public DateTime ScheduledShipDate { get; set; }

        [Required]
        public int RecipientID { get; set; }

        [StringLength(100)]
        public string Comments { get; set; }

        public string UserName { get; set; }

        public List<ProductSummaryViewModel> AvailableProductList { get; set; }
        public List<ProductSelectionItem> ProductSelection { get; set; }
        public List<SelectListItem> RecipientSelectionList { get; set; }
        public List<SelectListItem> CategoryList { get; set; }
    }
}
