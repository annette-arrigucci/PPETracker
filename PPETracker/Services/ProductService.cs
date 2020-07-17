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

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        //TODO: Method to create a view model for creating a new product

        //TODO: Method to create a new product
        /// <summary>
        /// Create a new product
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns>The id of the new product</returns>
        //public int CreateProduct
    }
}
