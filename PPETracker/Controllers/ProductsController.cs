using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PPETracker.Services;

namespace PPETracker.Controllers
{
    public class ProductsController : Controller
    {
        public ProductService _service; 

        public ProductsController(ProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult AddNew()
        {
            var model = _service.CreateProductInitialize();
            return View(model);
        }
    }
}
