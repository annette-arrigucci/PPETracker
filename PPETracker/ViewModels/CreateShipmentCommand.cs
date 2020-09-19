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
        public int RecipientID { get; set; }
        public string Comments { get; set; }
        public List<ProductSummaryViewModel> AvailableProductList { get; set; }
        public int[][] ProductSelection { get; set; }
    }
}
