using Microsoft.AspNetCore.Mvc.Rendering;
using PPETracker.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PPETracker.Services
{
    public class CategoryService
    {
        readonly ApplicationDbContext _context;
        string[] categoryArray = { "Canister", "Gas Mask", "Gloves", "Hand Sanitizer", "Mask", "Wipes", "Goggles", "Face Shield" };

        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<SelectListItem> GetCategoryList()
        {
            IEnumerable<SelectListItem> items = _context.Categories.Where(c => c.IsActive == true).
                Select(c => new SelectListItem { 
                    Text = c.Name, 
                    Value = c.ID.ToString() 
                });
            List<SelectListItem> listToReturn = items.ToList();
            return listToReturn;
        }

        public List<SelectListItem> GetCategoryNamesList()
        {
            IEnumerable<SelectListItem> items = _context.Categories.Where(c => c.IsActive == true).
                Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Name
                });
            List<SelectListItem> listToReturn = items.ToList();
            return listToReturn;
        }

        //method that will return Canister Type options to select from
        public List<SelectListItem> GetCanisterTypeOptions()
        {
            List<SelectListItem> canisterTypeList = new List<SelectListItem>
            {
                new SelectListItem { Text = "Air Purifying", Value = "Air Purifying" },
                new SelectListItem { Text = "Chemical Protective", Value = "Chemical Protective" }
            };
            return canisterTypeList;
        }

        //method that will return list of Gas Masks in database that user can associate a canister with
        public List<SelectListItem> GetGasMaskAssociatedWithOptions()
        {
            List<SelectListItem> gasMaskList = new List<SelectListItem>();
            List<string> allGasMaskList = new List<string>();

            //get gas masks from gas masks entered into tracker
            List<string> gasMaskListFromGasMasks = _context.GasMasks.Where(p => p.IsActive == true).Select(p => p.Name).Distinct().ToList();

            //get gas masks from gas masks that are associated with canisters
            List<string> gasMaskListFromCanisters = _context.Canisters.Where(p => p.IsActive == true).Select(p => p.GasMaskAssociatedWith).Distinct().ToList();

            //combine the two lists
            List<string> gasMaskListFromDB = gasMaskListFromGasMasks.Union(gasMaskListFromCanisters).ToList();

            //if there are gas masks in DB, form a list of SelectListItems from items in the list
            if (gasMaskListFromDB.Count != 0)
            {
                foreach (var gasMask in gasMaskListFromDB)
                {
                    SelectListItem item = new SelectListItem
                    {
                        Text = gasMask,
                        Value = gasMask
                    };
                    gasMaskList.Add(item);
                }

                //put "Other" at the end of the list
                SelectListItem otherItem = new SelectListItem
                {
                    Text = "Other",
                    Value = "Other"
                };
                gasMaskList.Add(otherItem);
            }

            //if no gas masks in DB, a list with count of 0 will be returned
            return gasMaskList;
        }

        //method that will return Gas Mask Type options to select from
        public List<SelectListItem> GetGasMaskTypeOptions()
        {
            List<SelectListItem> gasMaskTypeList = new List<SelectListItem>
            {
                new SelectListItem { Text = "Half-respirator", Value = "Half-respirator" },
                new SelectListItem { Text = "Full-respirator", Value = "Full-respirator" }
            };
            return gasMaskTypeList;
        }

        //method that will return Goggle Type options to select from
        public List<SelectListItem> GetGoggleTypeOptions()
        {
            List<SelectListItem> goggleTypeList = new List<SelectListItem>
            {
                new SelectListItem { Text = "Safety Glasses", Value = "Safety Glasses" },
                new SelectListItem { Text = "Sealable", Value = "Sealable" }
            };
            return goggleTypeList;
        }

        //method that will return Glove Size options to select from
        public List<SelectListItem> GetGloveSizeOptions()
        {
            List<SelectListItem> gloveSizeList = new List<SelectListItem>();
            List<string> allGloveSizeList = new List<string>();

            List<string> gloveSizeReferenceList = new List<string>
            {
                "S", "M", "L", "XL", "L/XL", "Fits most"
            };

            //get mask types from the Products table to see if users entered a new type to be included on the list
            List<string> gloveSizeListFromDB = _context.Gloves.Select(p => p.GloveSize).Distinct().ToList();

            if (gloveSizeListFromDB.Count != 0)
            {
                //merge mask types from the two lists
                allGloveSizeList = gloveSizeReferenceList.Union(gloveSizeListFromDB).ToList();
            }
            else
            {
                allGloveSizeList = gloveSizeReferenceList;
            }

            foreach (var gloveSize in allGloveSizeList)
            {
                SelectListItem item = new SelectListItem
                {
                    Text = gloveSize,
                    Value = gloveSize
                };
                gloveSizeList.Add(item);
            }

            //put "Other" at the end of the list
            SelectListItem otherItem = new SelectListItem
            {
                Text = "Other",
                Value = "Other"
            };
            gloveSizeList.Add(otherItem);

            return gloveSizeList;
        }

        //method that will return Sanitizer Type options to select from
        public List<SelectListItem> GetSanitizerTypeOptions()
        {
            List<SelectListItem> sanTypeList = new List<SelectListItem>
            {
                new SelectListItem { Text = "Gel", Value = "Gel" },
                new SelectListItem { Text = "Spray", Value = "Spray" }
            };
            return sanTypeList;
        }

        //method that will return Mask Type options to select from
        public List<SelectListItem> GetMaskTypeOptions()
        {
            List<SelectListItem> maskList = new List<SelectListItem>();
            List<string> allMaskTypeList = new List<string>();
            
            List<string> maskTypeReferenceList = new List<string>
            {
                "N-95", "KN-95", "KF-94", "Surgical", "Cloth"
            };

            //get mask types from the Products table to see if users entered a new type to be included on the list
            List<string> maskTypeListFromDB = _context.Masks.Where(p => p.IsActive == true).Select(p => p.MaskType).Distinct().ToList();

            if(maskTypeListFromDB.Count != 0)
            {
                //merge mask types from the two lists
                allMaskTypeList = maskTypeReferenceList.Union(maskTypeListFromDB).ToList();
            }
            else
            {
                allMaskTypeList = maskTypeReferenceList;
            }

            foreach(var maskType in allMaskTypeList)
            {
                SelectListItem item = new SelectListItem
                {
                    Text = maskType,
                    Value = maskType
                };
                maskList.Add(item);
            }

            //put "Other" at the end of the list
            SelectListItem otherItem = new SelectListItem
            {
                Text = "Other",
                Value = "Other"
            };
            maskList.Add(otherItem);

            return maskList;
        }

        //method that returns category name given a category ID
        public string GetCategoryName(int categoryID)
        {
            try
            {
                int catIndex = categoryID - 1;
                return categoryArray[catIndex];
            }
            catch
            {
                throw new Exception("Cannot find category name");
            }
        }
    }
}
