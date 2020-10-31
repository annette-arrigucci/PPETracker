using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PPETracker.Data;
using PPETracker.Models;
using PPETracker.ViewModels;

namespace PPETracker.Services
{
    public class ProductService
    {
        readonly ApplicationDbContext _context;
        readonly CategoryService _categoryService;
        private readonly IFactoryStrategy _factStrategy;

        public ProductService(ApplicationDbContext context, CategoryService categoryService, IFactoryStrategy factStrategy)
        {
            _context = context;
            _categoryService = categoryService;
            //object to create factories for create/update of appropriate type of object
            _factStrategy = factStrategy;
        }

        //Method to return list of products for dashboard
        public List<ProductSummaryViewModel> GetProducts()
        {
            var results = _context.Products.Where(p => p.IsActive == true)
                .Select(p => new ProductSummaryViewModel {
                    ID = p.ID,
                    CategoryID = p.CategoryID,
                    Name = p.Name,
                    Brand = p.Brand,
                    PhotoLink = p.PhotoLink,
                    Quantity = p.Quantity
            });
            var resultList = results.ToList();
            foreach(var r in resultList)
            {
                //look up category name
                var categoryName = _categoryService.GetCategoryName(r.CategoryID);
                r.CategoryName = categoryName;
            }
            return resultList;
        }

        //Method to return list of products for dashboard
        public List<ProductSummaryViewModel> GetAvailableProducts()
        {
            var results = _context.Products.Where(p => p.IsActive == true && p.Quantity > 0)
                .Select(p => new ProductSummaryViewModel
                {
                    ID = p.ID,
                    CategoryID = p.CategoryID,
                    Name = p.Name,
                    Brand = p.Brand,
                    PhotoLink = p.PhotoLink,
                    Quantity = p.Quantity
                });
            var resultList = results.ToList();
            foreach (var r in resultList)
            {
                //look up category name
                var categoryName = _categoryService.GetCategoryName(r.CategoryID);
                r.CategoryName = categoryName;
            }
            return resultList;
        }

        //Method to return list of deactivated products for dashboard
        public List<ProductSummaryViewModel> GetDeactivatedProducts()
        {
            var results = _context.Products.Where(p => p.IsActive == false)
                .Select(p => new ProductSummaryViewModel
                {
                    ID = p.ID,
                    CategoryID = p.CategoryID,
                    Name = p.Name,
                    Brand = p.Brand,
                    PhotoLink = p.PhotoLink,
                    Quantity = p.Quantity
                });
            var resultList = results.ToList();
            foreach (var r in resultList)
            {
                //look up category name
                var categoryName = _categoryService.GetCategoryName(r.CategoryID);
                r.CategoryName = categoryName;
            }
            return resultList;
        }

        //Method to create a view model for creating a new product
        public CreateProductCommand CreateProductInitialize()
        {
            CreateProductCommand prod = new CreateProductCommand();
            prod.CategoryOptions = _categoryService.GetCategoryList();
            prod.MaskTypeOptions = _categoryService.GetMaskTypeOptions();
            prod.SanitizerTypeOptions = _categoryService.GetSanitizerTypeOptions();
            prod.GloveSizeOptions = _categoryService.GetGloveSizeOptions();
            prod.GoggleTypeOptions = _categoryService.GetGoggleTypeOptions();
            prod.GasMaskTypeOptions = _categoryService.GetGasMaskTypeOptions();
            prod.CanisterTypeOptions = _categoryService.GetCanisterTypeOptions();
            prod.GasMaskAssociatedWithOptions = _categoryService.GetGasMaskAssociatedWithOptions();
            
            return prod;
        }

        /// <summary>
        /// Create a new product record
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns>The id of the new product</returns>
        public int CreateProduct(CreateProductCommand model)
        {
            //get the model data
            //based on the category of the product, use the Factory Strategy object to create and return an object
            //of the appropriate type
            //add the object to the appropriate table
            try
            {
                //create new Canister
                if (model.CategoryID == 1)
                {
                    Canister canisterToAdd = (Canister)_factStrategy.MakeProduct(model, CategoryName.Canister);
                    _context.Canisters.Add(canisterToAdd);
                    _context.SaveChanges();

                    return canisterToAdd.ID;
                }

                //create new Gas Mask
                if (model.CategoryID == 2)
                {
                    GasMask gasMaskToAdd = (GasMask)_factStrategy.MakeProduct(model, CategoryName.GasMask);
                    _context.GasMasks.Add(gasMaskToAdd);
                    _context.SaveChanges();

                    return gasMaskToAdd.ID;

                }

                //create new Gloves
                if (model.CategoryID == 3)
                {
                    Gloves glovesToAdd = (Gloves)_factStrategy.MakeProduct(model, CategoryName.Gloves);
                    _context.Gloves.Add(glovesToAdd);
                    _context.SaveChanges();

                    return glovesToAdd.ID;
                }

                //create new Hand Sanitizer
                if (model.CategoryID == 4)
                {
                    HandSanitizer sanToAdd = (HandSanitizer)_factStrategy.MakeProduct(model, CategoryName.HandSanitizer);
                    _context.HandSanitizers.Add(sanToAdd);
                    _context.SaveChanges();
                    return sanToAdd.ID;
                }

                //create new Mask
                if (model.CategoryID == 5)
                {
                    //create a new object of type Mask
                    //add this object to the database
                    Mask maskToAdd = (Mask)_factStrategy.MakeProduct(model, CategoryName.Mask);
                    _context.Masks.Add(maskToAdd);
                    _context.SaveChanges();

                    return maskToAdd.ID;
                }

                //create new Wipes
                if (model.CategoryID == 6)
                {
                    //create a new object of type Wipes
                    //add this object to the database
                    Wipes wipesToAdd = (Wipes)_factStrategy.MakeProduct(model, CategoryName.Wipes);
                    _context.Wipes.Add(wipesToAdd);
                    _context.SaveChanges();

                    return wipesToAdd.ID;
                }

                //create new Goggles
                if (model.CategoryID == 7)
                {
                    //create a new object of type Goggles
                    //add this object to the database
                    Goggles gogglesToAdd = (Goggles)_factStrategy.MakeProduct(model, CategoryName.Goggles);
                    _context.Goggles.Add(gogglesToAdd);
                    _context.SaveChanges();

                    return gogglesToAdd.ID;
                }

                //create new Face Shield
                if (model.CategoryID == 8)
                {
                    //create a new object of type Face Shield
                    //add this object to the database
                    FaceShield shieldToAdd = (FaceShield)_factStrategy.MakeProduct(model, CategoryName.FaceShield);
                    _context.FaceShields.Add(shieldToAdd);
                    _context.SaveChanges();

                    return shieldToAdd.ID;
                }

                return 0;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        /// <summary>
        /// Create a new product record
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns>The id of the new product</returns>
        public int UpdateProduct(UpdateProductCommand model)
        {
            //get the model data
            //based on the category of the product, use the Factory Strategy object to create and return an object
            //of the appropriate type
            //add the object to the appropriate table
            try
            {
                //update Canister
                if (model.CategoryID == 1)
                {
                    Canister canisterToEdit = _context.Canisters.Find(model.ID);
                    canisterToEdit = (Canister)_factStrategy.UpdateProduct(model, canisterToEdit, CategoryName.Canister);
                    
                    _context.SaveChanges();

                    return model.ID;
                }

                //update Gas Mask
                if (model.CategoryID == 2)
                {
                    GasMask gasMaskToEdit = _context.GasMasks.Find(model.ID);
                    gasMaskToEdit = (GasMask)_factStrategy.UpdateProduct(model, gasMaskToEdit, CategoryName.GasMask);

                    _context.SaveChanges();

                    return model.ID;
                }

                //update Gloves
                if (model.CategoryID == 3)
                {
                    Gloves glovesToEdit = _context.Gloves.Find(model.ID);
                    glovesToEdit = (Gloves)_factStrategy.UpdateProduct(model, glovesToEdit, CategoryName.Gloves);

                    _context.SaveChanges();

                    return model.ID;
                }

                //update Hand Sanitizer
                if (model.CategoryID == 4)
                {
                    HandSanitizer sanToEdit = _context.HandSanitizers.Find(model.ID);
                    sanToEdit = (HandSanitizer)_factStrategy.UpdateProduct(model, sanToEdit, CategoryName.HandSanitizer);

                    _context.SaveChanges();
                    return model.ID;
                }

                //update Mask
                if (model.CategoryID == 5)
                {
                    Mask maskToEdit = _context.Masks.Find(model.ID);
                    maskToEdit = (Mask)_factStrategy.UpdateProduct(model, maskToEdit, CategoryName.Mask);

                    _context.SaveChanges();

                    return model.ID;
                }

                //update Wipes
                if (model.CategoryID == 6)
                {
                    //create a new object of type Wipes
                    //add this object to the database
                    Wipes wipesToEdit = _context.Wipes.Find(model.ID);
                    wipesToEdit = (Wipes)_factStrategy.UpdateProduct(model, wipesToEdit, CategoryName.Wipes);

                    _context.SaveChanges();

                    return model.ID;
                }

                //update Goggles
                if (model.CategoryID == 7)
                {
                    //create a new object of type Goggles
                    //add this object to the database
                    Goggles gogglesToEdit = _context.Goggles.Find(model.ID);
                    gogglesToEdit = (Goggles)_factStrategy.UpdateProduct(model, gogglesToEdit, CategoryName.Goggles);

                    _context.SaveChanges();

                    return gogglesToEdit.ID;
                }

                //update Face Shield
                if (model.CategoryID == 8)
                {
                    //create a new object of type Face Shield
                    //add this object to the database
                    FaceShield shieldToEdit = _context.FaceShields.Find(model.ID);
                    shieldToEdit = (FaceShield)_factStrategy.UpdateProduct(model, shieldToEdit, CategoryName.FaceShield);

                    _context.SaveChanges();

                    return model.ID;
                }

                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public UpdateProductCommand GetProductForUpdate(int productID)
        {
            //look up the product ID
            var itemToEdit = _context.Products.Where(p => p.ID == productID).Select(p => p).FirstOrDefault();
            //if null, throw exception
            if(itemToEdit == null)
            {
                throw new Exception("Product not found");
            }
            //check for IsActive flag
            if(itemToEdit.IsActive == false)
            {
                throw new Exception("Product not active");
            }
            //get the category
            int categoryID = itemToEdit.CategoryID;

            //initialize a new model
            UpdateProductCommand model = new UpdateProductCommand();
            //depending on the category, call the method in the appropriate factory class
            //canister
            if(categoryID == 1)
            {
                model = _factStrategy.MakeEditViewModel(itemToEdit, CategoryName.Canister);
                model.CanisterTypeOptions = _categoryService.GetCanisterTypeOptions();
                model.GasMaskAssociatedWithOptions = _categoryService.GetGasMaskAssociatedWithOptions();
            }
            //gas mask
            else if(categoryID == 2)
            {
                model = _factStrategy.MakeEditViewModel(itemToEdit, CategoryName.GasMask);
                model.GasMaskTypeOptions = _categoryService.GetGasMaskTypeOptions();
            }
            //gloves
            else if(categoryID == 3)
            {
                model = _factStrategy.MakeEditViewModel(itemToEdit, CategoryName.Gloves);
                model.GloveSizeOptions = _categoryService.GetGloveSizeOptions();
            }
            //hand sanitizer
            else if (categoryID == 4)
            {
                model = _factStrategy.MakeEditViewModel(itemToEdit, CategoryName.HandSanitizer);
                model.SanitizerTypeOptions = _categoryService.GetSanitizerTypeOptions();
            }
            //mask
            else if (categoryID == 5)
            {
                model = _factStrategy.MakeEditViewModel(itemToEdit, CategoryName.Mask);
                model.MaskTypeOptions = _categoryService.GetMaskTypeOptions();
            }
            //wipes
            else if (categoryID == 6)
            {
                model = _factStrategy.MakeEditViewModel(itemToEdit, CategoryName.Wipes);
            }
            //goggles
            else if (categoryID == 7)
            {
                model = _factStrategy.MakeEditViewModel(itemToEdit, CategoryName.Goggles);
                model.GoggleTypeOptions = _categoryService.GetGoggleTypeOptions();
            }
            //face shield
            else if (categoryID == 8)
            {
                model = _factStrategy.MakeEditViewModel(itemToEdit, CategoryName.FaceShield);
            }
            //throw exception if Category not valid
            else
            {
                throw new Exception("Category not valid");
            }

            //return the view model
            return model;
        }

        public ProductDetailViewModel GetProductDetail(int productID)
        {
            //look up the product ID
            var selectedProd = _context.Products.Where(p => p.ID == productID).Select(p => p).FirstOrDefault();
            //if null, throw exception
            if (selectedProd == null)
            {
                throw new Exception("Product not found");
            }
            //do not check for IsActive flag - can request details on a deactivated product
           
            //get the category
            int categoryID = selectedProd.CategoryID;

            //canister
            if (categoryID == 1)
            {
                //create new view model using factory 
                ProductDetailViewModel prodDetailItem = _factStrategy.MakeDetailViewModel(selectedProd, CategoryName.Canister);
                return prodDetailItem;
            }

            //gas mask
            else if (categoryID == 2)
            {
                ProductDetailViewModel prodDetailItem = _factStrategy.MakeDetailViewModel(selectedProd, CategoryName.GasMask);
                return prodDetailItem;
            }

            //gloves
            else if (categoryID == 3)
            {
                ProductDetailViewModel prodDetailItem = _factStrategy.MakeDetailViewModel(selectedProd, CategoryName.Gloves);
                return prodDetailItem;
            }

            //hand sanitizer
            else if (categoryID == 4)
            {
                //create new view model using factory 
                ProductDetailViewModel prodDetailItem = _factStrategy.MakeDetailViewModel(selectedProd, CategoryName.HandSanitizer);
                return prodDetailItem;
            }

            //mask
            else if (categoryID == 5)
            {
                ProductDetailViewModel prodDetailItem = _factStrategy.MakeDetailViewModel(selectedProd, CategoryName.Mask);
                return prodDetailItem;
            }

            //wipes
            else if (categoryID == 6)
            {
                ProductDetailViewModel prodDetailItem = _factStrategy.MakeDetailViewModel(selectedProd, CategoryName.Wipes);
                return prodDetailItem;
            }

            //goggles
            else if (categoryID == 7)
            {
                ProductDetailViewModel prodDetailItem = _factStrategy.MakeDetailViewModel(selectedProd, CategoryName.Goggles);
                return prodDetailItem;
            }

            //face shield
            else if (categoryID == 8)
            {
                ProductDetailViewModel prodDetailItem = _factStrategy.MakeDetailViewModel(selectedProd, CategoryName.FaceShield);
                return prodDetailItem;
            }
            else
            {
                throw new Exception("Category not found");
            }
        }

        public void UpdateProductQuantity(int productID, int quantity)
        {
            try
            {
                //look up the product ID
                var itemToUpdate = _context.Products.Where(p => p.ID == productID).Select(p => p).FirstOrDefault();
                //if null, throw exception
                if (itemToUpdate == null)
                {
                    throw new Exception("Product not found");
                }
                //check for IsActive flag
                if (itemToUpdate.IsActive == false)
                {
                    throw new Exception("Product not active");
                }

                //update the quantity
                itemToUpdate.Quantity = quantity;
                _context.SaveChanges();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        //Get product quantity
        public int GetProductQuantity(int productID)
        {
            try
            {
                //look up the product ID
                var result = _context.Products.Where(p => p.ID == productID).Select(p => p).FirstOrDefault();
                //if null, throw exception
                if (result == null)
                {
                    throw new Exception("Product not found");
                }
                //check for IsActive flag
                if (result.IsActive == false)
                {
                    throw new Exception("Product not active");
                }

                //return the quantity
                return result.Quantity;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        //Get product name
        public string GetProductName(int productID)
        {
            try
            {
                //look up the product ID
                var result = _context.Products.Where(p => p.ID == productID && p.IsActive == true).Select(p => p.Name).FirstOrDefault();
                //if null, throw exception
                if (result == null)
                {
                    throw new Exception("Product not found or is not active");
                }
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        //deactivate product
        public void DeactivateProduct(int productID)
        {
            try
            {
                //look up the product ID
                var itemToUpdate = _context.Products.Where(p => p.ID == productID).Select(p => p).FirstOrDefault();
                //if null, throw exception
                if (itemToUpdate == null)
                {
                    throw new Exception("Product not found");
                }
                //check for IsActive flag
                if (itemToUpdate.IsActive == false)
                {
                    throw new Exception("Product not active");
                }

                //set the Is Active flag to false
                itemToUpdate.IsActive = false;
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        //reactivate product
        public void ReactivateProduct(int productID)
        {
            try
            {
                //look up the product ID
                var itemToUpdate = _context.Products.Where(p => p.ID == productID).Select(p => p).FirstOrDefault();
                //if null, throw exception
                if (itemToUpdate == null)
                {
                    throw new Exception("Product not found");
                }
                //check for IsActive flag
                if (itemToUpdate.IsActive == true)
                {
                    throw new Exception("Error - product already active");
                }

                //set the Is Active flag to true
                itemToUpdate.IsActive = true;
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        //returns a string describing status of product
        public string IsProductIDValid(int productID)
        {
            try
            {
                var item = _context.Products.Where(p => p.ID == productID).Select(p => p).FirstOrDefault();
                //if null, throw exception
                if (item == null)
                {
                    return "Not found";
                }
                //check for IsActive flag
                else if (item.IsActive == false)
                {
                    return "Not active";
                }
                else
                {
                    return "Active";
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
            

        public string GetPhotoLink(int productID)
        {
            //check if product ID is valid
            string prodStatus = IsProductIDValid(productID);
            if (prodStatus == "Not found")
            {
                throw new Exception("Product not found");
            }
            if (prodStatus == "Not active")
            {
                throw new Exception("Product not active");
            }
            var photoLink = _context.Products.Where(p => p.ID == productID).Select(p => p.PhotoLink).FirstOrDefault();
            //return field from table - can be null
            return photoLink;
        }

        //get category ID of product
        public int GetCategoryID(int productID)
        {
            try
            {
                int? catID = _context.Products.Where(p => p.ID == productID).Select(p => p.CategoryID).FirstOrDefault();
                //if null, throw exception
                if (catID == null)
                {
                    throw new Exception("No category for product");
                }
                else
                {
                    return (int)catID;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
    }
}
