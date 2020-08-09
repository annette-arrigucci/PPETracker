using PPETracker.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PPETracker.Models
{
    public class HandSanitizerFactory : IFactory
    {
        public CategoryName CategoryName => CategoryName.HandSanitizer;

        public ProductDetailViewModel MakeDetailViewModel(Product productToDisplay)
        {
            HandSanitizer handSanProd = (HandSanitizer)productToDisplay;
            ProductDetailViewModel model = new ProductDetailViewModel();
            model.Name = handSanProd.Name;
            model.ID = handSanProd.ID;
            model.Brand = handSanProd.Brand;
            model.CategoryID = handSanProd.CategoryID;
            model.CategoryName = "Hand Sanitizer";
            model.Comments = handSanProd.Comments;
            model.PhotoLink = handSanProd.PhotoLink;
            model.NumOunces = handSanProd.NumOunces;
            model.SanitizerType = handSanProd.SanitizerType;
            model.Quantity = handSanProd.Quantity;
            return model;
        }

        public UpdateProductCommand MakeEditViewModel(Product productToEdit)
        {
            HandSanitizer handSanProd = (HandSanitizer)productToEdit;
            UpdateProductCommand model = new UpdateProductCommand();
            model.Name = handSanProd.Name;
            model.ID = handSanProd.ID;
            model.Brand = handSanProd.Brand;
            model.CategoryID = handSanProd.CategoryID;
            model.Comments = handSanProd.Comments;
            model.PhotoLink = handSanProd.PhotoLink;
            model.NumOunces = handSanProd.NumOunces;
            model.SanitizerType = handSanProd.SanitizerType;
            return model;
        }

        public Product MakeProduct(CreateProductCommand model)
        {
            HandSanitizer handSanitizerModel = new HandSanitizer
            {
                Brand = model.Brand,
                CategoryID = model.CategoryID,
                Comments = model.Comments,
                DateCreated = DateTime.Now,
                IsActive = true,
                SanitizerType = model.SanitizerType,
                NumOunces = (int)model.NumOunces,
                Name = model.Name,
                PhotoLink = model.PhotoLink,
                Quantity = 0
            };
            return handSanitizerModel;
        }
    }
}
