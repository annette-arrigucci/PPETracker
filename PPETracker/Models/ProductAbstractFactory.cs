using PPETracker.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PPETracker.Models
{
    public abstract class ProductAbstractFactory
    {
        protected abstract Product MakeProduct(CreateProductCommand model);
    }
}
