using PPETracker.Data;
using PPETracker.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PPETracker.Services
{
    public class ShipmentService
    {
        readonly ApplicationDbContext _context;
        readonly CategoryService _categoryService;
        readonly ProductService _productService;

        public ShipmentService(ApplicationDbContext context, CategoryService categoryService, ProductService productService)
        {
            _context = context;
            _categoryService = categoryService;
            _productService = productService;
        }

        public CreateShipmentCommand GetCreateModelWithProducts()
        {
            var products = _productService.GetProducts();
            CreateShipmentCommand model = new CreateShipmentCommand();
            model.AvailableProductList = products;
            return model;
        }

        /*private int[][] InitializeProductSelection(List<ProductSummaryViewModel> prodList)
        {
            int[][] arrayToReturn = prodList.Select(p => new
            {
                ID = p.ID,
                Quantity = 0
            }).ToArray();
        }*/
        //TODO: Add Create Shipment method
        //TODO: Add Create Shipment Product method
        //TODO: Add Edit Shipment methods
        //TODO: Add Delete Shipment methods
        //TODO: Add View Shipment methods
        //TODO: Add View all shipment records method
    }
}
