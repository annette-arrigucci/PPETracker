using PPETracker.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PPETracker.Models
{
    public class GlovesFactory : IFactory
    {
        public CategoryName CategoryName => CategoryName.Gloves;

        public ProductDetailViewModel MakeDetailViewModel(Product productToDisplay)
        {
            Gloves glovesProd = (Gloves)productToDisplay;
            ProductDetailViewModel model = new ProductDetailViewModel();
            model.Name = glovesProd.Name;
            model.ID = glovesProd.ID;
            model.Brand = glovesProd.Brand;
            model.CategoryID = glovesProd.CategoryID;
            model.CategoryName = "Gloves";
            model.Comments = glovesProd.Comments;
            model.PhotoLink = glovesProd.PhotoLink;
            model.GloveQuantity = glovesProd.GloveQuantity;
            model.GloveSize = glovesProd.GloveSize;
            model.GloveThickness = glovesProd.GloveThickness;
            model.Quantity = glovesProd.Quantity;
            return model;
        }

        public UpdateProductCommand MakeEditViewModel(Product productToEdit)
        {
            Gloves glovesProd = (Gloves)productToEdit;
            UpdateProductCommand model = new UpdateProductCommand();
            model.Name = glovesProd.Name;
            model.ID = glovesProd.ID;
            model.Brand = glovesProd.Brand;
            model.CategoryID = glovesProd.CategoryID;
            model.CategoryName = "Gloves";
            model.Comments = glovesProd.Comments;
            model.PhotoLink = glovesProd.PhotoLink;
            model.GloveQuantity = glovesProd.GloveQuantity;
            model.GloveSize = glovesProd.GloveSize;
            model.GloveThickness = glovesProd.GloveThickness;
            return model;
        }

        public Product MakeProduct(CreateProductCommand model)
        {
            //if the user entered a new glove size type, make that the Glove Size entry
            if (model.GloveSize == "Other")
            {
                model.GloveSize = model.UserEnteredGloveSize;
            }

            Gloves glovesModel = new Gloves
            {
                Brand = model.Brand,
                CategoryID = model.CategoryID,
                Comments = model.Comments,
                DateCreated = DateTime.Now,
                IsActive = true,
                GloveQuantity = (int)model.GloveQuantity,
                GloveThickness = model.GloveThickness,
                GloveSize = model.GloveSize,
                Name = model.Name,
                PhotoLink = model.PhotoLink,
                Quantity = 0
            };
            return glovesModel;
        }

        public Product UpdateProduct(UpdateProductCommand model, Product productToUpdate)
        {
            //if the user entered a new glove size type, make that the Glove Size entry
            if (model.GloveSize == "Other")
            {
                model.GloveSize = model.UserEnteredGloveSize;
            }

            Gloves updatedGloves = (Gloves)productToUpdate;
            updatedGloves.Brand = model.Brand;
            updatedGloves.Comments = model.Comments;
            updatedGloves.DateModified = DateTime.Now;
            updatedGloves.GloveQuantity = (int)model.GloveQuantity;
            updatedGloves.GloveThickness = model.GloveThickness;
            updatedGloves.GloveSize = model.GloveSize;
            updatedGloves.Name = model.Name;
            updatedGloves.PhotoLink = model.PhotoLink;
            return updatedGloves;
        }
    }
}
