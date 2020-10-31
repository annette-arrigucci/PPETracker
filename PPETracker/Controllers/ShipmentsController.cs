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
                model.AvailableProductList = _service.GetAvailableProducts();
                model.ProductSelection = _shipService.InitializeProductSelection(model.AvailableProductList);
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

        /*[HttpGet]
        public IActionResult Edit(int shipmentID)
        {
            //don't allow edit if shipment has shipped
            CreateShipmentCommand model = _shipService.GetEditModelWithProducts(shipmentID);
            return View(model);
        }*/

        /*[HttpPost]
        public IActionResult Edit(EditShipmentCommand model)
        {
            //model Validations go here
            //check shipping date
            if (model.ScheduledShipDate < DateTime.Now.AddMonths(-6))
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
                model.AvailableProductList = _service.GetAvailableProducts();
                model.ProductSelection = _shipService.InitializeProductSelection(model.AvailableProductList);
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
        }*/

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
            //TODO: add delete method here

            //redirect to dashboard
            return RedirectToAction("Dashboard", "Shipments");
        }
    }
}
