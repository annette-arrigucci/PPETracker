using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PPETracker.ViewModels
{
    public class ShipmentSummaryViewModel
    {
        [Display(Name = "ID")]
        public int ID { get; set; }

        //status is "Y" or "N"
        [Display(Name = "Shipped")]
        public string ShippedStatus { get; set; }

        [Display(Name = "Scheduled Ship Date")]
        public DateTime ScheduledShipDate { get; set; }

        [Display(Name = "Actual Ship Date")]
        public DateTime? ActualShipDate { get; set; }

        public int RecipientID { get; set; }

        [Display(Name = "Recipient")]
        public string RecipientName { get; set; }

    }
}
