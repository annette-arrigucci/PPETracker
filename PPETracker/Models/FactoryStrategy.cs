using PPETracker.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PPETracker.Models
{
    public class FactoryStrategy : IFactoryStrategy
    {
        private readonly IEnumerable<IFactory> _factories;

        public FactoryStrategy(IEnumerable<IFactory> factories)
        {
            _factories = factories;
        }

        public ProductDetailViewModel MakeDetailViewModel(Product product, CategoryName categoryName)
        {
            return _factories.FirstOrDefault(x => x.CategoryName == categoryName)?.MakeDetailViewModel(product) ?? throw new ArgumentNullException(nameof(categoryName));
        }

        public UpdateProductCommand MakeEditViewModel(Product product, CategoryName categoryName)
        {
            return _factories.FirstOrDefault(x => x.CategoryName == categoryName)?.MakeEditViewModel(product) ?? throw new ArgumentNullException(nameof(categoryName));
        }

        public Product MakeProduct(CreateProductCommand model, CategoryName categoryName)
        {
            return _factories.FirstOrDefault(x => x.CategoryName == categoryName)?.MakeProduct(model) ?? throw new ArgumentNullException(nameof(categoryName));
        }

        public Product UpdateProduct(UpdateProductCommand model, Product productToUpdate, CategoryName categoryName)
        {
            return _factories.FirstOrDefault(x => x.CategoryName == categoryName)?.UpdateProduct(model, productToUpdate) ?? throw new ArgumentNullException(nameof(categoryName));
        }
    }
}
