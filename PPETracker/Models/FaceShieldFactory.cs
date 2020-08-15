using PPETracker.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PPETracker.Models
{
    public class FaceShieldFactory : IFactory
    {
        public CategoryName CategoryName => CategoryName.FaceShield;

        public ProductDetailViewModel MakeDetailViewModel(Product productToDisplay)
        {
            FaceShield faceShieldProd = (FaceShield)productToDisplay;
            ProductDetailViewModel model = new ProductDetailViewModel();
            model.Name = faceShieldProd.Name;
            model.ID = faceShieldProd.ID;
            model.Brand = faceShieldProd.Brand;
            model.PhotoLink = faceShieldProd.PhotoLink;
            model.CategoryID = faceShieldProd.CategoryID;
            model.CategoryName = "Face Shield";
            model.Comments = faceShieldProd.Comments;
            model.Quantity = faceShieldProd.Quantity;
            return model;
        }

        public UpdateProductCommand MakeEditViewModel(Product productToEdit)
        {
            FaceShield faceShieldProd = (FaceShield)productToEdit;
            UpdateProductCommand model = new UpdateProductCommand();
            model.Name = faceShieldProd.Name;
            model.ID = faceShieldProd.ID;
            model.Brand = faceShieldProd.Brand;
            model.CategoryID = faceShieldProd.CategoryID;
            model.CategoryName = "Face Shield";
            model.Comments = faceShieldProd.Comments;
            model.PhotoLink = faceShieldProd.PhotoLink;
            return model;
        }

        public Product MakeProduct(CreateProductCommand model)
        {
            FaceShield faceShieldModel = new FaceShield
            {
                Brand = model.Brand,
                CategoryID = model.CategoryID,
                Comments = model.Comments,
                DateCreated = DateTime.Now,
                IsActive = true,
                Name = model.Name,
                PhotoLink = model.PhotoLink,
                Quantity = 0
            };
            return faceShieldModel;
        }

        public Product UpdateProduct(UpdateProductCommand model, Product productToUpdate)
        {
            FaceShield updatedShield = (FaceShield)productToUpdate;
            updatedShield.Brand = model.Brand;
            updatedShield.Comments = model.Comments;
            updatedShield.DateModified = DateTime.Now;
            updatedShield.Name = model.Name;
            updatedShield.PhotoLink = model.PhotoLink;
            return updatedShield;
        }
    }
}
