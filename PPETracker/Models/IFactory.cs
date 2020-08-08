using PPETracker.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PPETracker.Models
{
    public interface IFactory
    {
        CategoryName CategoryName { get; }
        Product MakeProduct(CreateProductCommand model);
        UpdateProductCommand MakeEditViewModel(Product productToEdit);
        ProductDetailViewModel MakeDetailViewModel(Product productToDisplay);
    }
}
