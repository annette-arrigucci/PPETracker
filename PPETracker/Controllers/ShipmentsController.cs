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
            if(model.ScheduledShipDate < DateTime.Today)
            {
                ModelState.AddModelError("ScheduledShipDate", "Date cannot be before today's date");
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
                return RedirectToAction("Dashboard", "Products");
            }
        }
    }
}
