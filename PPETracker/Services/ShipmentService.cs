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
            //only include products that are in stock
            var availProducts = products.Where(p => p.Quantity > 0).ToList();
            CreateShipmentCommand model = new CreateShipmentCommand();
            //set ship date default to today
            model.ScheduledShipDate = DateTime.Now;
            model.AvailableProductList = availProducts;
            model.ProductSelection = InitializeProductSelection(availProducts);
            model.RecipientSelectionList = GetRecipientList();
            model.CategoryList = _categoryService.GetCategoryNamesList();
            return model;
        }

        public List<ProductSelectionItem> InitializeProductSelection(List<ProductSummaryViewModel> prodList)
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

        //return name of recipient given a recipient ID
        public string GetRecipientName(int ID)
        {
            string recName = _context.Recipients.Where(p => p.ID == ID).Select(p => p.Name).FirstOrDefault();
            return recName;
        }

        //Add Create Shipment method
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
            _context.SaveChanges();
            return shipmentToAdd.ID;
        }

        public List<ProductSelectionItem> GetSelectedProducts(List<ProductSelectionItem> productQuantityList)
        {
            //get a list of all products that have quantity greater than zero
            var selectedList = productQuantityList.Where(p => p.QuantityForOrder > 0).Select(p => p).ToList();
            return selectedList;
        }

        public List<string> CheckSelectedProducts(List<ProductSelectionItem> selectedProductList)
        {
            //validate each selection on the list
            List<string> errorMessages = new List<string>();

            //validate that product ID is valid, validate that quantity selected is less than or equal to quantity available
            foreach (var item in selectedProductList)
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
        public ShipmentDetailViewModel GetShipmentDetails(int shipmentID)
        {
            var detItem = _context.Shipments
                .Where(p => p.ID == shipmentID)
                .Select(p => new ShipmentDetailViewModel
                {
                    ID = p.ID,
                    RecipientID = p.RecipientID,
                    ShipStatus = p.ShipStatus,
                    ScheduledShipDate = p.ScheduledShipDate,
                    ActualShipDate = p.ActualShipDate,
                    Comments = p.Comments
                }).FirstOrDefault();
            //look up recipient name
            detItem.RecipientName = GetRecipientName(detItem.RecipientID);

            //look up product details
            //get list of products on shipment
            var shipProducts = _context.ShipmentProducts
                .Where(p => p.ShipmentID == detItem.ID)
                .Select(p => new ProductSummaryForShipment { 
                    ID = p.ProductID,
                    QuantityOnShipment = p.Quantity
                })
                .ToList();

            //for each product on shipment, get the product name
            foreach(var prod in shipProducts)
            {
                prod.Name = _productService.GetProductName(prod.ID);
            }

            detItem.ProductsOnShipment = shipProducts;

            return detItem;
        }
        public List<ShipmentSummaryViewModel> GetShipments()
        {
            var results = _context.Shipments
                .Select(p => new ShipmentSummaryViewModel
                {
                    ID = p.ID,
                    ShippedStatus = p.ShipStatus,
                    ScheduledShipDate = p.ScheduledShipDate,
                    ActualShipDate = p.ActualShipDate,
                    RecipientID = p.RecipientID,
                });
            var resultList = results.ToList();
            foreach (var r in resultList)
            {
                //look up recipient ID
                var recName = GetRecipientName(r.RecipientID);
                r.RecipientName = recName;
            }
            return resultList;
        }
        //Add View all shipment records method

        //check whether shipment ID is in table
        public bool IsShipmentIDValid(int shipmentID)
        {
            var result = _context.Shipments.Where(p => p.ID == shipmentID).FirstOrDefault();
            if(result == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //check whether shipment has already shipped
        public bool IsShipmentShipped(int shipmentID)
        {
            var result = _context.Shipments.Where(p => p.ID == shipmentID).Select(p => p.ShipStatus).FirstOrDefault();
            if(result == "Y")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Change status of shipment to Shipped
        public void ChangeStatusToShipped(int shipmentID)
        {
            //find the shipment
            var shipmentToShip = _context.Shipments.Where(p => p.ID == shipmentID).FirstOrDefault();
            //change shipped status
            shipmentToShip.ShipStatus = "Y";
            //add shipping data
            shipmentToShip.ActualShipDate = DateTime.Now;
            _context.SaveChanges();
        }

        //Ship products on shipment - remove them from inventory
        public void ShipProductsOnShipment(int shipmentID)
        {
            //find the shipment
            var shipmentToShip = _context.Shipments.Where(p => p.ID == shipmentID).FirstOrDefault();
            //find the shipment products to ship
            var productsToShip = _context.ShipmentProducts.Where(p => p.ShipmentID == shipmentID).Select(p => p).ToList();

            //for each product, update the quantity in the Products table
            foreach(var product in productsToShip)
            {
                //get the current product quantity
                var currentQuantity = _productService.GetProductQuantity(product.ProductID);

                //get the quantity for shipment
                var quantityForShipment = product.Quantity;

                //subtract the quantity selected for shipment
                var updatedQuantity = currentQuantity - quantityForShipment;

                //throw error if invalid updated quantity
                if(updatedQuantity < 0)
                {
                    //get product name
                    var prodName = _productService.GetProductName(product.ProductID);
                    throw new Exception("Error shipping " + prodName + ". Quantity selected for shipment exceeds what is available.");
                }

                //update the product quantity
                _productService.UpdateProductQuantity(product.ProductID, updatedQuantity);
            }
            _context.SaveChanges();
        }

    }
}
