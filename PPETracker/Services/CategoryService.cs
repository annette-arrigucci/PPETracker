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
    }
}
