using PPETracker.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PPETracker.Models
{
    public abstract class ProductAbstractFactory
    {
        public abstract Product MakeProduct(CreateProductCommand model);
        public abstract UpdateProductCommand MakeEditViewModel(int productID);
    }
}
