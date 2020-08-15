using PPETracker.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PPETracker.Models
{
    public interface IFactoryStrategy
    {
        Product MakeProduct(CreateProductCommand model, CategoryName categoryName);
        Product UpdateProduct(UpdateProductCommand model, Product productToUpdate, CategoryName categoryName);
        UpdateProductCommand MakeEditViewModel(Product product, CategoryName categoryName);

        ProductDetailViewModel MakeDetailViewModel(Product product, CategoryName categoryName);
    }
}
