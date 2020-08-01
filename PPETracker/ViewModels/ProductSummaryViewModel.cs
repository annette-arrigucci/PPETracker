using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PPETracker.ViewModels
{
    public class ProductSummaryViewModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public int CategoryID { get; set; }

        public string CategoryName { get; set; }

        public string PhotoLink { get; set; }

        public string Brand { get; set; }

        public int Quantity { get; set; }
    }
}
