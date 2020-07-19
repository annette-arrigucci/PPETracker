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

        //Method to create a view model for creating a new product
        public CreateProductCommand CreateProductInitialize()
        {
            CreateProductCommand prod = new CreateProductCommand();
            prod.CategoryOptions = _categoryService.GetCategoryList();
            prod.MaskTypeOptions = _categoryService.GetMaskTypeOptions();
            return prod;
        }

        /// <summary>
        /// Create a new product record
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns>The id of the new product</returns>
        /*public int CreateProduct(CreateProductCommand model)
        {
            //get the model data
            //based on the category of the product, create and return the appropriate object
            //add the object to the appropriate table
            //if(model.CategoryID == 5){
                //create a new object of type Mask
                //add this object to the database
            }
        }*/
    }
}
