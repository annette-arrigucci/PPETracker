using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PPETracker.Models;
using PPETracker.Services;
using PPETracker.ViewModels;

namespace PPETracker.Controllers
{
    [Authorize(AuthenticationSchemes = "Identity.Application", Policy = "IsAdmin")]
    public class ShipmentsController : Controller
    {
        public ProductService _service;
        public CategoryService _catService;
        public ShipmentService _shipService;
        private readonly IWebHostEnvironment _environment;
        public UserManager<ApplicationUser> _userManager;

        public ShipmentsController(ProductService service, CategoryService catService, ShipmentService shipService, IWebHostEnvironment environment, UserManager<ApplicationUser> userManager)
        {
            _service = service;
            _catService = catService;
            _shipService = shipService;
            _environment = environment;
            _userManager = userManager;
        }

        public IActionResult Dashboard()
        {
            var model = _shipService.GetShipments();
            return View(model);
        }

        [HttpGet]
        public IActionResult Details(int? shipmentID)
        {
            if(shipmentID == null)
            {
                throw new Exception("Invalid shipment ID");
            }
            ShipmentDetailViewModel model = _shipService.GetShipmentDetails((int)shipmentID);
            return PartialView("_Details", model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            CreateShipmentCommand model = _shipService.GetCreateModelWithProducts();
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(CreateShipmentCommand model)
        {
            //model Validations go here
            //check shipping date
            if(model.ScheduledShipDate < DateTime.Now.AddMonths(-6))
            {
                ModelState.AddModelError("ScheduledShipDate", "Date cannot be older than six months ago");
            }

            //get the selected products from the list that was returned
            List<ProductSelectionItem> selectedProducts = _shipService.GetSelectedProducts(model.ProductSelection);

            //check that each shipment product selection is valid
            List<string> errorMessages = _shipService.CheckSelectedProducts(selectedProducts);
            foreach (var message in errorMessages)
            {
                ModelState.AddModelError("", message);
            }

            if (!ModelState.IsValid)
            {
                //get available product detail list
                var allAvailableIDs = _service.GetAvailableProductIDs();
                model.AvailableProductList = _service.GetAvailableProductDetailList(allAvailableIDs);

                //get hidden all product list
                //initialize product selection with empty selected list
                List<ProductSelectionItem> selectedItems = new List<ProductSelectionItem>();
                model.ProductSelection = _shipService.InitializeProductSelection(allAvailableIDs, selectedItems);
                                
                model.RecipientSelectionList = _shipService.GetRecipientList();
                model.CategoryList = _catService.GetCategoryNamesList();
                return View(model);
            }
            else
            {
                //Create the shipment
                //get the username
                model.UserName = User.Identity.Name;

                //Get the shipment ID 
                int shipmentID = _shipService.CreateShipment(model);
                
                _shipService.CreateShipmentProductRecords(shipmentID, selectedProducts);

                //Redirect to Shipments Dashboard
                return RedirectToAction("Dashboard", "Shipments");
            }
        }

        [HttpGet]
        public IActionResult Edit(int? shipmentID)
        {
            //check for null
            if (shipmentID == null)
            {
                throw new Exception("Invalid shipment ID");
            }
            //check if shipment ID is in table
            bool isValid = _shipService.IsShipmentIDValid((int)shipmentID);
            if (isValid == false)
            {
                throw new Exception("Shipment ID not found");
            }

            //check if shipment is shipped
            bool isShipped = _shipService.IsShipmentShipped((int)shipmentID);
            if (isShipped == true)
            {
                throw new Exception("Error - cannot edit shipped shipment");
            }

            EditShipmentCommand model = _shipService.GetEditModelWithProducts((int)shipmentID);
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EditShipmentCommand model)
        {
            //model Validations go here
            //check shipping date
            if (model.ScheduledShipDate < DateTime.Now.AddMonths(-6))
            {
                ModelState.AddModelError("ScheduledShipDate", "Date cannot be older than six months ago");
            }

            //get the selected products from the updated shipment
            List<ProductSelectionItem> selectedItems = _shipService.GetSelectedProducts(model.ProductSelection);

            //check that each product selection is valid
            List<string> errorMessages = _shipService.CheckSelectedProducts(selectedItems);
            foreach (var message in errorMessages)
            {
                ModelState.AddModelError("", message);
            }

            if (!ModelState.IsValid)
            {
                //if not valid, need to pass back the available and selected lists for display and the hidden list that 
                //stores what the user selected 
                //initialize a list of ProductSummaryForShipment
                List<ProductSummaryForShipment> prodSummaryList = new List<ProductSummaryForShipment>();
                foreach (var item in selectedItems)
                {
                    //create a new ProductSummaryForShipment item
                    var prodSummaryItem = new ProductSummaryForShipment
                    {
                        ID = item.ProductID,
                        QuantityOnShipment = item.QuantityForOrder
                    };
                    prodSummaryList.Add(prodSummaryItem);
                }
                //for the list that was created, call method that will fill in product details for each item
                model.SelectedProductList = _service.GetSelectedProductDetailList(prodSummaryList);

                //get details for the available products to redisplay
                var allAvailableIDs = _service.GetAvailableProductIDs();

                //get list of selected product IDs
                var selectedIDs = prodSummaryList.Select(p => p.ID).ToList();
                var nonSelectedIDs = allAvailableIDs.Except(selectedIDs).ToList();

                //get details for non-selected items
                model.AvailableProductList = _service.GetAvailableProductDetailList(nonSelectedIDs);

                model.RecipientSelectionList = _shipService.GetRecipientList();
                model.CategoryList = _catService.GetCategoryNamesList();
                return View(model);
            }
            else
            {
                //get the username
                model.UserName = User.Identity.Name;

                //update the shipment
                _shipService.UpdateShipment(model);

                //get the previously selected items on the shipment
                List<ProductSelectionItem> prevSelectedItems = _shipService.GetProductsOnShipment(model.ID);

                // get IDs of previously selected products
                var prevSelectedIDs = prevSelectedItems.Select(p => p.ProductID).ToList();

                //get a list of new product IDs that were added
                var selectedIDs = selectedItems.Select(p => p.ProductID).ToList();

                //add shipment content records for added products
                var addedProductIDs = selectedIDs.Except(prevSelectedIDs).ToList();

                //get Product Selection Item objects for added products
                List<ProductSelectionItem> addedList = selectedItems.Where(p => addedProductIDs.Contains(p.ProductID)).ToList();

                _shipService.CreateShipmentProductRecords(model.ID, addedList);

                //get items to remove
                var prodsToRemove = prevSelectedIDs.Except(selectedIDs).ToList();

                //for each previously selected item, check if the quantity changed in the new selection
                foreach(var item in prevSelectedItems)
                {
                    int oldQuant = item.QuantityForOrder;
                    var updateItem = selectedItems.Where(p => p.ProductID == item.ProductID).FirstOrDefault();
                    if(item.QuantityForOrder != updateItem.QuantityForOrder)
                    {
                        _shipService.UpdateShipmentProductQty(model.ID, item.ProductID, updateItem.QuantityForOrder);
                    }
                }

                //Redirect to Shipments Dashboard
                return RedirectToAction("Dashboard", "Shipments");
            }
        }

        [HttpGet]
        public IActionResult Ship(int? shipmentID)
        {
            //check if shipment ID is valid
            if(shipmentID == null)
            {
                throw new Exception("Invalid Shipment ID");
            }

            //check if shipment ID is in table
            bool isValid = _shipService.IsShipmentIDValid((int)shipmentID);
            if(isValid == false)
            {
                throw new Exception("Shipment ID not found");
            }

            //check if shipment is already shipped
            bool isShipped = _shipService.IsShipmentShipped((int)shipmentID);
            if(isShipped == true)
            {
                throw new Exception("Error - shipment already shipped");
            }

            //look up shipment details to display
            var model = _shipService.GetShipmentDetails((int)shipmentID);
            if(model.ProductsOnShipment.Count == 0)
            {
                ViewBag.HasZeroItems = true;
            }
            else
            {
                ViewBag.HasZeroItems = false;
            }
            return PartialView("_Ship", model);
        }

        [HttpPost]
        public IActionResult Ship(int ID)
        {
            //check if shipment ID is in table
            bool isValid = _shipService.IsShipmentIDValid((int)ID);
            if (isValid == false)
            {
                throw new Exception("Shipment ID not found");
            }

            //check if there are zero items on shipment
            int numItemsOnShipment = _shipService.GetNumItemsOnShipment(ID);
            if(numItemsOnShipment == 0)
            {
                throw new Exception("Cannot ship shipment with zero items");
            }

            //change the status of the shipment
            _shipService.ChangeStatusToShipped(ID);
            //ship the products in the shipment - remove them from the inventory
            _shipService.ShipProductsOnShipment(ID);
            return RedirectToAction("Dashboard", "Shipments");
        }

        [HttpGet]
        public IActionResult Delete(int? shipmentID)
        {
            if (shipmentID == null)
            {
                throw new Exception("Invalid shipment ID");
            }
            ShipmentDetailViewModel model = _shipService.GetShipmentDetails((int)shipmentID);
            return PartialView("_Delete", model);
        }

        [HttpPost]
        public IActionResult Delete(int ID)
        {
            //check if shipment ID is in table
            bool isValid = _shipService.IsShipmentIDValid((int)ID);
            if (isValid == false)
            {
                throw new Exception("Shipment ID not found");
            }

            //get shipment product records
            var shipProdList = _shipService.GetProductsOnShipment(ID);
            var shipProdIDList = shipProdList.Select(p => p.ProductID).ToList();

            //delete shipment product records
            _shipService.RemoveProductsFromShipment(ID, shipProdIDList);

            //delete shipment
            _shipService.DeleteShipment(ID);

            //redirect to dashboard
            return RedirectToAction("Dashboard", "Shipments");
        }
    }
}
