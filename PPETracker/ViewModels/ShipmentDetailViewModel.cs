using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PPETracker.ViewModels
{
    public class ShipmentDetailViewModel
    {
        [Display(Name = "Shipment ID")]
        public int ID { get; set; }

        public int RecipientID { get; set; }

        [Display(Name = "Recipient Name")]
        public string RecipientName { get; set; }

        [Display(Name = "Shipped")]
        public string ShipStatus { get; set; }

        [Display(Name = "Scheduled Ship Date")]
        public DateTime ScheduledShipDate { get; set; }

        [Display(Name = "Actual Ship Date")]
        public DateTime? ActualShipDate { get; set; }

        [Display(Name = "Comments")]
        public string Comments { get; set; }
        public List<ProductSummaryForShipment> ProductsOnShipment { get; set; }
    }
}
