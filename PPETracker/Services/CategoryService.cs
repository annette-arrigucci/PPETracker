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
            List<string> maskTypeListFromDB = _context.Masks.Select(p => p.MaskType).Distinct().ToList();

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
    }
}
