using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Permissions;

namespace PPETracker.Models
{
    public class Shipment
    {
        public int ID { get; set; }
        public DateTime ScheduledShipDate { get; set; }
        public DateTime? ActualShipDate { get; set; }
        public int RecipientID { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string ShipStatus { get; set; }
        public string Comments { get; set; }
        public string UserID { get; set; }

        public ICollection<ShipmentProduct> ShipmentProducts { get; set; }
        public Recipient Recipient { get; set; }
    }
}
