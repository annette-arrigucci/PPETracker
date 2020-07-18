using System;
using System.Collections.Generic;
using System.Linq;
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
            prod.CategorySelections = _categoryService.GetCategoryList();
            return prod;
        }

        //TODO: Method to create a new product
        /// <summary>
        /// Create a new product
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns>The id of the new product</returns>
        //public int CreateProduct
    }
}
