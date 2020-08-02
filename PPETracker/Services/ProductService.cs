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

        public ProductService(ApplicationDbContext context, CategoryService categoryService)
        {
            _context = context;
            _categoryService = categoryService;
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
            //based on the category of the product, create and return the appropriate object
            //add the object to the appropriate table

            //create new Canister
            if(model.CategoryID == 1)
            {
                CanisterConcreteFactory canisterFactory = new CanisterConcreteFactory();
                Canister canisterToAdd = (Canister)canisterFactory.MakeProduct(model);
                _context.Canisters.Add(canisterToAdd);
                _context.SaveChanges();

                return canisterToAdd.ID;
            }

            //create new Gas Mask
            if (model.CategoryID == 2)
            {
                GasMaskConcreteFactory gasMaskFactory = new GasMaskConcreteFactory();
                GasMask gasMaskToAdd = (GasMask)gasMaskFactory.MakeProduct(model);
                _context.GasMasks.Add(gasMaskToAdd);
                _context.SaveChanges();

                return gasMaskToAdd.ID;
            }

            //create new Gloves
            if (model.CategoryID == 3)
            {
                GlovesConcreteFactory glovesFactory = new GlovesConcreteFactory();
                Gloves glovesToAdd = (Gloves)glovesFactory.MakeProduct(model);
                _context.Gloves.Add(glovesToAdd);
                _context.SaveChanges();

                return glovesToAdd.ID;
            }

            //create new Hand Sanitizer
            if (model.CategoryID == 4)
            {
                HandSanitizerConcreteFactory sanFactory = new HandSanitizerConcreteFactory();
                HandSanitizer sanToAdd = (HandSanitizer)sanFactory.MakeProduct(model);
                _context.HandSanitizers.Add(sanToAdd);
                _context.SaveChanges();

                return sanToAdd.ID;
            }

            //create new Mask
            if(model.CategoryID == 5){
                //create a new object of type Mask
                //add this object to the database
                MaskConcreteFactory maskFactory = new MaskConcreteFactory();
                Mask maskToAdd = (Mask)maskFactory.MakeProduct(model);
                _context.Masks.Add(maskToAdd);
                _context.SaveChanges();

                return maskToAdd.ID;
            }

            //create new Wipes
            if (model.CategoryID == 6)
            {
                //create a new object of type Wipes
                //add this object to the database
                WipesConcreteFactory wipesFactory = new WipesConcreteFactory();
                Wipes wipesToAdd = (Wipes)wipesFactory.MakeProduct(model);
                _context.Wipes.Add(wipesToAdd);
                _context.SaveChanges();

                return wipesToAdd.ID;
            }

            //create new Goggles
            if (model.CategoryID == 7)
            {
                //create a new object of type Goggles
                //add this object to the database
                GogglesConcreteFactory gogglesFactory = new GogglesConcreteFactory();
                Goggles gogglesToAdd = (Goggles)gogglesFactory.MakeProduct(model);
                _context.Goggles.Add(gogglesToAdd);
                _context.SaveChanges();

                return gogglesToAdd.ID;
            }

            return 0;
        }

        /*public UpdateProductCommand GetProductForUpdate(int productID)
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
            
            //depending on the category, call the method in the appropriate factory class
            if(categoryID == 1)
            {
                var canisterFactory = new CanisterConcreteFactory();

            }

            //add the dropdown information to the model
            //return the view model
        }*/
    }
}
