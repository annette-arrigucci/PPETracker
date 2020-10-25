using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PPETracker.ViewModels { 
    //view model to show product that is on shipment and its quantity
    public class ProductSummaryForShipment : ProductSummaryViewModel
    {
        public int QuantityOnShipment { get; set; }
    }
}
