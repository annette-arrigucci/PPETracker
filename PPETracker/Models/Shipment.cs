using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Permissions;

namespace PPETracker.Models
{
    public class Shipment
    {
        public int ID { get; set; }

        [Required]
        public DateTime ScheduledShipDate { get; set; }
        public DateTime? ActualShipDate { get; set; }
        public int RecipientID { get; set; }

        [Required]
        [StringLength(1)]
        public string ShipStatus { get; set; }

        [StringLength(100)]
        public string Comments { get; set; }

        [StringLength(256)]
        public string UserName { get; set; }

        public ICollection<ShipmentProduct> ShipmentProducts { get; set; }
        public Recipient Recipient { get; set; }
    }
}
