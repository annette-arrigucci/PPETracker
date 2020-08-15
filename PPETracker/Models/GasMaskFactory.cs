using PPETracker.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PPETracker.Models
{
    public class GasMaskFactory : IFactory
    {
        public CategoryName CategoryName => CategoryName.GasMask;

        public ProductDetailViewModel MakeDetailViewModel(Product productToDisplay)
        {
            GasMask gasMaskProd = (GasMask)productToDisplay;
            ProductDetailViewModel model = new ProductDetailViewModel();
            model.Name = gasMaskProd.Name;
            model.ID = gasMaskProd.ID;
            model.Brand = gasMaskProd.Brand;
            model.CategoryID = gasMaskProd.CategoryID;
            model.CategoryName = "Gas Mask";
            model.PhotoLink = gasMaskProd.PhotoLink;
            model.Comments = gasMaskProd.Comments;
            model.GasMaskType = gasMaskProd.GasMaskType;
            model.Quantity = gasMaskProd.Quantity;
            return model;
        }

        public UpdateProductCommand MakeEditViewModel(Product productToEdit)
        {
            GasMask gasMaskProd = (GasMask)productToEdit;
            UpdateProductCommand model = new UpdateProductCommand();
            model.Name = gasMaskProd.Name;
            model.ID = gasMaskProd.ID;
            model.Brand = gasMaskProd.Brand;
            model.CategoryID = gasMaskProd.CategoryID;
            model.Comments = gasMaskProd.Comments;
            model.GasMaskType = gasMaskProd.GasMaskType;
            return model;
        }

        public Product MakeProduct(CreateProductCommand model)
        {
            GasMask gasMaskModel = new GasMask
            {
                Brand = model.Brand,
                CategoryID = model.CategoryID,
                Comments = model.Comments,
                DateCreated = DateTime.Now,
                IsActive = true,
                GasMaskType = model.GasMaskType,
                Name = model.Name,
                PhotoLink = model.PhotoLink,
                Quantity = 0
            };
            return gasMaskModel;
        }
    }
}
