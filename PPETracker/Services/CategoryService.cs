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

        //method that will return Mask Type options to select from
        public List<SelectListItem> GetMaskTypeOptions()
        {
            List<SelectListItem> maskList = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "N-95",
                    Value = "N-95"
                },
                new SelectListItem
                {
                    Text = "KN-95",
                    Value = "KN-95"
                },
                new SelectListItem
                {
                    Text = "KF-94",
                    Value = "KF-94"
                },
                new SelectListItem
                {
                    Text = "Surgical",
                    Value = "Surgical"
                },
                new SelectListItem
                {
                    Text = "Cloth",
                    Value = "Cloth"
                },
                new SelectListItem
                {
                    Text = "Other",
                    Value = "Other"
                }
            };
            return maskList;
        }
    }
}
