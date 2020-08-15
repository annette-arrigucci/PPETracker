using PPETracker.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PPETracker.Models
{
    public class CanisterFactory : IFactory
    {
        public CategoryName CategoryName => CategoryName.Canister;

        public ProductDetailViewModel MakeDetailViewModel(Product productToDisplay)
        {
            Canister canisterProd = (Canister)productToDisplay;
            ProductDetailViewModel model = new ProductDetailViewModel();
            model.Name = canisterProd.Name;
            model.ID = canisterProd.ID;
            model.Brand = canisterProd.Brand;
            model.CategoryID = canisterProd.CategoryID;
            model.CategoryName = "Canister";
            model.Comments = canisterProd.Comments;
            model.CanisterType = canisterProd.CanisterType;
            model.GasMaskAssociatedWith = canisterProd.GasMaskAssociatedWith;
            model.Quantity = canisterProd.Quantity;
            return model;
        }

        public UpdateProductCommand MakeEditViewModel(Product productToEdit)
        {
            Canister canisterProd = (Canister)productToEdit;
            UpdateProductCommand model = new UpdateProductCommand();
            model.Name = canisterProd.Name;
            model.ID = canisterProd.ID;
            model.Brand = canisterProd.Brand;
            model.CategoryID = canisterProd.CategoryID;
            model.CategoryName = "Canister";
            model.PhotoLink = canisterProd.PhotoLink;
            model.Comments = canisterProd.Comments;
            model.CanisterType = canisterProd.CanisterType;
            model.GasMaskAssociatedWith = canisterProd.GasMaskAssociatedWith;
            return model;
        }

        public Product MakeProduct(CreateProductCommand model)
        {
            //if the user did not choose a gas mask option from the list, assume they entered 
            //a gas mask name
            if (model.GasMaskAssociatedWith == "Other")
            {
                model.GasMaskAssociatedWith = model.UserEnteredGasMaskAssociatedWith;
            }
            if (model.GasMaskAssociatedWith == null)
            {
                model.GasMaskAssociatedWith = model.UserEnteredGasMaskAssociatedWith;
            }
            Canister canisterModel = new Canister
            {
                Brand = model.Brand,
                CategoryID = model.CategoryID,
                Comments = model.Comments,
                DateCreated = DateTime.Now,
                IsActive = true,
                CanisterType = model.CanisterType,
                GasMaskAssociatedWith = model.GasMaskAssociatedWith,
                Name = model.Name,
                PhotoLink = model.PhotoLink,
                Quantity = 0
            };
            return canisterModel;
        }

        public Product UpdateProduct(UpdateProductCommand model, Product productToUpdate)
        {
            //if the user did not choose a gas mask option from the list, assume they entered 
            //a gas mask name
            if (model.GasMaskAssociatedWith == "Other")
            {
                model.GasMaskAssociatedWith = model.UserEnteredGasMaskAssociatedWith;
            }
            if (model.GasMaskAssociatedWith == null)
            {
                model.GasMaskAssociatedWith = model.UserEnteredGasMaskAssociatedWith;
            }
            Canister updatedCanister = (Canister)productToUpdate;
            updatedCanister.Brand = model.Brand;
            updatedCanister.Comments = model.Comments;
            updatedCanister.DateModified = DateTime.Now;
            updatedCanister.CanisterType = model.CanisterType;
            updatedCanister.GasMaskAssociatedWith = model.GasMaskAssociatedWith;
            updatedCanister.Name = model.Name;
            updatedCanister.PhotoLink = model.PhotoLink;
            return updatedCanister;
        }
    }
}
