using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PPETracker.ViewModels
{
    public class CreateProductCommand
    {
        public string Name { get; set; }
        public int CategoryID { get; set; }
        //add dropdown data for category
        public string PhotoLink { get; set; }
        public string Brand { get; set; }
        public string Comments { get; set; }
        public int Quantity { get; set; }

    }
}
