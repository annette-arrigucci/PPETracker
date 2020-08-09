using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using PPETracker.Models;
using PPETracker.Services;
using PPETracker.ViewModels;

namespace PPETracker.Controllers
{
    public class ProductsController : Controller
    {
        public ProductService _service;
        public CategoryService _catService;
        private readonly IWebHostEnvironment _environment;

        public ProductsController(ProductService service, CategoryService catService, IWebHostEnvironment environment)
        {
            _service = service;
            _catService = catService;
            _environment = environment;
        }

        [HttpGet]
        public IActionResult Dashboard()
        {
            var model = _service.GetProducts();
            ViewBag.CategoryOptions = _catService.GetCategoryNamesList();
            return View(model);
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
                    ModelState.AddModelError("File", "Invalid Format");

                //make sure the file name is less than 50 characters
                if (model.File.FileName.Length >= 50)
                {
                    ModelState.AddModelError("File", "File name too long");
                }

                //make sure the file is less than 2MB
                if (model.File.Length > 2000000)
                {
                    ModelState.AddModelError("File", "File must be less than 2MB");
                }
            }
            //do checks here for data that is required for certain categories
            //Gas mask canister checks
            if(model.CategoryID == 1)
            {
                //check that canister type has been selected
                if(model.CanisterType == null)
                {
                    ModelState.AddModelError("CanisterType", "Canister type must be selected");
                }

                //check options for gas mask type
                var gasMaskTypeList = _catService.GetGasMaskAssociatedWithOptions();
                if(gasMaskTypeList.Count > 0)
                {
                    //check that gas mask associated with option has been selected
                    if (model.GasMaskAssociatedWith == null && model.UserEnteredGasMaskAssociatedWith == null)
                    {
                        ModelState.AddModelError("GasMaskAssociatedWith", "Must select or enter an associated gas mask");
                    }

                    //if "other" was selected for gas mask, check that user entered text in text box
                    if (model.GasMaskAssociatedWith == "Other" && model.UserEnteredGasMaskAssociatedWith == null)
                    {
                        ModelState.AddModelError("UserEnteredGasMaskAssociatedWith", "Must enter a gas mask name");
                    }
                }
                else
                {
                    //if no dropdown for gas mask type, check that user entered a gas mask name
                    if(model.UserEnteredGasMaskAssociatedWith == null)
                    {
                        ModelState.AddModelError("UserEnteredGasMaskAssociatedWith", "Must enter a gas mask name");
                    }
                }
            }

            //Gas mask checks
            if (model.CategoryID == 2)
            {
                //check that gas mask type has been selected
                if (model.GasMaskType == null)
                {
                    ModelState.AddModelError("GasMaskType", "Gas Mask Type must be selected");
                }
            }

            //Glove checks
            if (model.CategoryID == 3)
            {
                //glove thickness is not required - can be null

                if(model.GloveQuantity == null)
                {
                    ModelState.AddModelError("GloveQuantity", "Glove Quantity is required");
                }

                //check that the glove size has been selected
                if (model.GloveSize == null)
                {
                    ModelState.AddModelError("GloveSize", "Glove Size must be selected");
                }
                //if the user selected to enter a new size, validate that the entered size is not null
                if(model.GloveSize == "Other")
                {
                    if(model.UserEnteredGloveSize == null)
                    {
                        ModelState.AddModelError("UserEnteredGloveSize", "Please enter a glove size.");
                    }
                }
            }

            //Hand Sanitizer checks
            if (model.CategoryID == 4)
            {
                //check that sanitizer type has been selected
                if(model.SanitizerType == null)
                {
                    ModelState.AddModelError("SanitizerType", "Sanitizer Type must be selected");
                }
                if(model.NumOunces == null)
                {
                    ModelState.AddModelError("NumOunces", "Number of ounces is required");
                }
            }

            //Mask checks
            if(model.CategoryID == 5)
            {
                //check that the mask type has been selected
                if(model.MaskType == null)
                {
                    ModelState.AddModelError("MaskType", "Mask type must be selected");
                }
                //if the user selected to enter a new type, validate that the entered mask is not null
                if(model.MaskType == "Other")
                {
                    if(model.UserEnteredMaskType == null)
                    {
                        ModelState.AddModelError("UserEnteredMaskType", "Please enter a mask type.");
                    }
                }
            }

            //Wipes checks
            if (model.CategoryID == 6)
            {
                //check that wipe quantity has been entered
                if (model.WipeQuantity == null)
                {
                    ModelState.AddModelError("WipeQuantity", "Wipe Quantity is required");
                }
            }

            //Goggles checks
            if (model.CategoryID == 7)
            {
                //check that goggles type has been selected
                if (model.GogglesType == null)
                {
                    ModelState.AddModelError("GogglesType", "Goggles Type must be selected");
                }
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
                    var absPath = Path.Combine(_environment.WebRootPath, "uploads", newFileName);
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
            //get dropdown options for categories and category-specific types
            model.CategoryOptions = _catService.GetCategoryList();
            model.SanitizerTypeOptions = _catService.GetSanitizerTypeOptions();
            model.MaskTypeOptions = _catService.GetMaskTypeOptions();
            model.GloveSizeOptions = _catService.GetGloveSizeOptions();
            model.GoggleTypeOptions = _catService.GetGoggleTypeOptions();
            model.GasMaskTypeOptions = _catService.GetGasMaskTypeOptions();
            model.CanisterTypeOptions = _catService.GetCanisterTypeOptions();
            model.GasMaskAssociatedWithOptions = _catService.GetGasMaskAssociatedWithOptions();

            return View(model);
        }

        public IActionResult Edit(int? ID)
        {
            if(ID == null)
            {
                throw new Exception("Invalid Product ID");
            }
            //get info for product with Product ID
            var model = _service.GetProductForUpdate((int)ID);
            return View(model);
        }

        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UpdateProductCommand model)
        {
            //check if product ID is valid
            string prodStatus = _service.IsProductIDValid(model.ID);
            if(prodStatus == "Not found")
            {
                throw new Exception("Product not found");
            }
            if (prodStatus == "Not active")
            {
                throw new Exception("Product not active");
            }
            //get old photo link from old version of record
            string prevPhotoLink = _service.GetPhotoLink(model.ID);

            //check for change in photo link 
            // - photo link removed
            if(prevPhotoLink != null && model.PhotoLink == null)
            {
                //delete the uploaded photo
            }
            // - new photo uploaded to replace old photo
            if(prevPhotoLink != null && model.File != null)
            {
                //delete the old photo
                //upload the new photo
            }
            // - new photo uploaded where previously none had been
            if(prevPhotoLink == null && model.File != null)
            {
                //upload the new photo
            }

            return View(model);
        }*/

        [HttpGet]
        public IActionResult UpdateProductQuantity(int? productID)
        {
            if (productID == null)
            {
                throw new Exception("Invalid Product ID");
            }
            //get info for product with Product ID
            var model = _service.GetProductDetail((int)productID);
            return PartialView("_UpdateProductQuantity", model);
        }

        //Method to handle POST request to update product quantity
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateProductQuantity([Bind(nameof(ProductDetailViewModel.ID), nameof(ProductDetailViewModel.Quantity))] ProductDetailViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Error", "Invalid data");
            }
            else
            {
                _service.UpdateProductQuantity(model.ID, model.Quantity);
                return RedirectToAction("Dashboard");
            }
        }

    }
}
