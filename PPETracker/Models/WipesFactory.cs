using PPETracker.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PPETracker.Models
{
    public class WipesFactory : IFactory
    {
        public CategoryName CategoryName => CategoryName.Wipes;

        public ProductDetailViewModel MakeDetailViewModel(Product productToDisplay)
        {
            Wipes wipesProd = (Wipes)productToDisplay;
            ProductDetailViewModel model = new ProductDetailViewModel();
            model.Name = wipesProd.Name;
            model.ID = wipesProd.ID;
            model.Brand = wipesProd.Brand;
            model.CategoryID = wipesProd.CategoryID;
            model.CategoryName = "Wipes";
            model.Comments = wipesProd.Comments;
            model.PhotoLink = wipesProd.PhotoLink;
            model.WipeQuantity = wipesProd.WipeQuantity;
            model.Quantity = wipesProd.Quantity;
            return model;
        }

        public UpdateProductCommand MakeEditViewModel(Product productToEdit)
        {
            Wipes wipesProd = (Wipes)productToEdit;
            UpdateProductCommand model = new UpdateProductCommand();
            model.Name = wipesProd.Name;
            model.ID = wipesProd.ID;
            model.Brand = wipesProd.Brand;
            model.CategoryID = wipesProd.CategoryID;
            model.Comments = wipesProd.Comments;
            model.PhotoLink = wipesProd.PhotoLink;
            model.WipeQuantity = wipesProd.WipeQuantity;
            return model;
        }

        public Product MakeProduct(CreateProductCommand model)
        {
            Wipes wipesModel = new Wipes
            {
                Brand = model.Brand,
                CategoryID = model.CategoryID,
                Comments = model.Comments,
                DateCreated = DateTime.Now,
                IsActive = true,
                WipeQuantity = (int)model.WipeQuantity,
                Name = model.Name,
                PhotoLink = model.PhotoLink,
                Quantity = 0
            };
            return wipesModel;
        }
    }
}