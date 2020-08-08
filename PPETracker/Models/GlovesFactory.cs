﻿using PPETracker.ViewModels;
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
            throw new NotImplementedException();
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
    }
}