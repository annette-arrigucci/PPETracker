using Microsoft.AspNetCore.Mvc.Rendering;
using PPETracker.Data;
using PPETracker.Models;
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
            model.ProductSelection = InitializeProductSelection(products);
            model.RecipientSelectionList = GetRecipientList();
            model.CategoryList = _categoryService.GetCategoryList();
            return model;
        }

        private List<ProductSelectionItem> InitializeProductSelection(List<ProductSummaryViewModel> prodList)
        {
            List<int> prodIDList = prodList.Select(p => p.ID).ToList();
            List<ProductSelectionItem> prodListToReturn = new List<ProductSelectionItem>();
            for(int i = 0; i < prodIDList.Count; i++)
            {
                var prodSelectItem = new ProductSelectionItem { ProductID = prodIDList[i], QuantityForOrder = 0 };
                prodListToReturn.Add(prodSelectItem);
            }
            return prodListToReturn;
        }

        //return list of all possible shipment recipients
        public List<SelectListItem> GetRecipientList()
        {
            List<SelectListItem> recipientsList = _context.Recipients.Where(p => p.IsActive == true).Select(p => 
                new SelectListItem { 
                    Text = p.Name,
                    Value = p.ID.ToString()
                }).ToList();
            return recipientsList;
        }
        //TODO: Add Create Shipment method
        public int CreateShipment(CreateShipmentCommand model)
        {
            Shipment shipmentToAdd = new Shipment
            {
                ScheduledShipDate = model.ScheduledShipDate,
                ShipStatus = "N",
                RecipientID = model.RecipientID,
                Comments = model.Comments,
                UserName = model.UserName
            };
            _context.Shipments.Add(shipmentToAdd);
            return shipmentToAdd.ID;
        }

        public List<string> CheckSelectedProducts(List<ProductSelectionItem> selectedProductList)
        {
            //validate each selection on the list
            List<string> errorMessages = new List<string>();
            //get a list of all products that have quantity greater than zero
            var selectedList = selectedProductList.Where(p => p.QuantityForOrder > 0).Select(p => p).ToList();
            //validate that product ID is valid, validate that quantity selected is less than or equal to quantity available
            foreach (var item in selectedList)
            {
                string productValidMessage = _productService.IsProductIDValid(item.ProductID);
                if(productValidMessage != "Active")
                {
                    errorMessages.Add("Product ID " + item.ProductID + "cannot be added because it is " + productValidMessage + ". Please contact an administrator.");
                }

                //otherwise check quantity
                //get available quantity for product
                int quantityAvailable = _productService.GetProductQuantity(item.ProductID);
                //if selected quantity exceeds what is available, add error message
                if(item.QuantityForOrder > quantityAvailable)
                {
                    errorMessages.Add("Unable to ship " + item.QuantityForOrder + " of Product ID " + item.ProductID 
                        + ". There are " + quantityAvailable + " units of product available.");
                }
            }
            return errorMessages;
        }

        //Add Create Shipment Product method
        public void CreateShipmentProductRecords(int shipmentID, List<ProductSelectionItem> prodSelectList)
        {
            foreach(var item in prodSelectList)
            {
                ShipmentProduct shipProd = new ShipmentProduct
                {
                    ShipmentID = shipmentID,
                    ProductID = item.ProductID,
                    Quantity = item.QuantityForOrder
                };
                _context.ShipmentProducts.Add(shipProd);
                _context.SaveChanges();
            }
        }
        //TODO: Add Edit Shipment methods
        //TODO: Add Delete Shipment methods
        //TODO: Add View Shipment methods 
        //TODO: Add View all shipment records method
    }
}
