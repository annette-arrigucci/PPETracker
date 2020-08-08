﻿using PPETracker.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PPETracker.Models
{
    public class GogglesFactory : IFactory
    {
        public CategoryName CategoryName => CategoryName.Goggles;

        public ProductDetailViewModel MakeDetailViewModel(Product productToDisplay)
        {
            Goggles gogglesProd = (Goggles)productToDisplay;
            ProductDetailViewModel model = new ProductDetailViewModel();
            model.Name = gogglesProd.Name;
            model.ID = gogglesProd.ID;
            model.Brand = gogglesProd.Brand;
            model.CategoryID = gogglesProd.CategoryID;
            model.CategoryName = "Goggles";
            model.Comments = gogglesProd.Comments;
            model.PhotoLink = gogglesProd.PhotoLink;
            model.GogglesType = gogglesProd.GogglesType;
            model.Quantity = gogglesProd.Quantity;
            return model;
        }

        public UpdateProductCommand MakeEditViewModel(int productToEdit)
        {
            throw new NotImplementedException();
        }

        public UpdateProductCommand MakeEditViewModel(Product productToEdit)
        {
            throw new NotImplementedException();
        }

        public Product MakeProduct(CreateProductCommand model)
        {
            Goggles gogglesModel = new Goggles
            {
                Brand = model.Brand,
                CategoryID = model.CategoryID,
                Comments = model.Comments,
                DateCreated = DateTime.Now,
                IsActive = true,
                GogglesType = model.GogglesType,
                Name = model.Name,
                PhotoLink = model.PhotoLink,
                Quantity = 0
            };
            return gogglesModel;
        }
    }
}