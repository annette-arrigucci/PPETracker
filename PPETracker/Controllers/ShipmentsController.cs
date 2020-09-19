using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PPETracker.Models;
using PPETracker.Services;
using PPETracker.ViewModels;

namespace PPETracker.Controllers
{
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

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            CreateShipmentCommand model = _shipService.GetCreateModelWithProducts();
            return View(model);
        }
    }
}
