using PPETracker.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PPETracker.Models
{
    public class MaskFactory : IFactory
    {
        public CategoryName CategoryName => CategoryName.Mask;

        public ProductDetailViewModel MakeDetailViewModel(Product productToDisplay)
        {
            Mask maskProd = (Mask)productToDisplay;
            ProductDetailViewModel model = new ProductDetailViewModel();
            model.Name = maskProd.Name;
            model.ID = maskProd.ID;
            model.Brand = maskProd.Brand;
            model.CategoryID = maskProd.CategoryID;
            model.CategoryName = "Mask";
            model.Comments = maskProd.Comments;
            model.PhotoLink = maskProd.PhotoLink;
            model.MaskType = maskProd.MaskType;
            model.Quantity = maskProd.Quantity;
            return model;
        }

        public UpdateProductCommand MakeEditViewModel(Product productToEdit)
        {
            Mask maskProd = (Mask)productToEdit;
            UpdateProductCommand model = new UpdateProductCommand();
            model.Name = maskProd.Name;
            model.ID = maskProd.ID;
            model.Brand = maskProd.Brand;
            model.CategoryID = maskProd.CategoryID;
            model.Comments = maskProd.Comments;
            model.PhotoLink = maskProd.PhotoLink;
            model.MaskType = maskProd.MaskType;
            return model;
        }

        public Product MakeProduct(CreateProductCommand model)
        {
            //if the user entered a new mask type, make that the Mask Type entry
            if (model.MaskType == "Other")
            {
                model.MaskType = model.UserEnteredMaskType;
            }

            Mask maskModel = new Mask
            {
                Brand = model.Brand,
                CategoryID = model.CategoryID,
                Comments = model.Comments,
                DateCreated = DateTime.Now,
                IsActive = true,
                MaskType = model.MaskType,
                Name = model.Name,
                PhotoLink = model.PhotoLink,
                Quantity = 0
            };
            return maskModel;
        }
    }
}
