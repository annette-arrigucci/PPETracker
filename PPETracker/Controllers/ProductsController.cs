using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using PPETracker.Services;
using PPETracker.ViewModels;

namespace PPETracker.Controllers
{
    public class ProductsController : Controller
    {
        public ProductService _service;
        private readonly IWebHostEnvironment _environment;

        public ProductsController(ProductService service, IWebHostEnvironment environment)
        {
            _service = service;
            _environment = environment;
        }

        [HttpGet]
        public IActionResult AddNew()
        {
            var model = _service.CreateProductInitialize();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddNew(CreateProductCommand model)
        {
            //do checks for image file here, return error message if invalid
            if (model.File != null && model.File.Length > 0)
            {  //check the file name to make sure its an image                 
                var ext = Path.GetExtension(model.File.FileName).ToLower();
                if (ext != ".png" && ext != ".jpg" && ext != ".jpeg" && ext != ".gif" && ext != ".bmp")
                    ModelState.AddModelError("PhotoLink", "Invalid Format");

                //make sure the file name is less than 50 characters
                if (model.File.FileName.Length >= 50)
                {
                    ModelState.AddModelError("PhotoLink", "File name too long");
                }

                //make sure the file is less than 2MB
                if (model.File.Length > 2000000)
                {
                    ModelState.AddModelError("PhotoLink", "File must be less than 2MB");
                }
            }
            //do checks here for data that is required for certain categories, 
            //i.e. user has selected a mask type if this is in the Mask category
            if(model.CategoryID == 5)
            {
                //TODO: check that the mask type has been selected
                //TODO: if the user selected to enter a new type, validate that the entered mask is not null
            }

            if (ModelState.IsValid)
            {
                //save image file to "uploads" folder if valid
                if (model.File != null)
                {
                    //trim extension on filename
                    var trimFileName = Path.GetFileNameWithoutExtension(model.File.FileName);
                    //get unique identifier for file name
                    Random rnd = new Random();
                    var uniqueFileId = Convert.ToString(rnd.Next(100000, 1000000));
                    var fileExtension = Path.GetExtension(model.File.FileName);
                    var newFileName = trimFileName + "_" + uniqueFileId + fileExtension;
                    //get absolute path for file save
                    var absPath = Path.Combine(_environment.WebRootPath, "ImageUploads", newFileName);
                    //relative path to store in folder in database
                    model.PhotoLink = "/uploads/" + newFileName;
                    using (FileStream fs = new FileStream(absPath, FileMode.Create))
                    {
                        model.File.CopyTo(fs);
                    }
                }
                //send the model to Product Service to save to database
                _service.CreateProduct(model);
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
    }
}
