using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PPETracker.ViewModels
{
    public class ProductSummaryViewModel
    {
        public int ID { get; set; }

        public string Name { get; set; }
        
        public int CategoryID { get; set; }

        [Display(Name = "Category")]
        public string CategoryName { get; set; }

        [Display(Name = "Photo")]
        public string PhotoLink { get; set; }

        public string Brand { get; set; }

        public int Quantity { get; set; }
    }
}
