using PPETracker.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PPETracker.Models
{
    public class WipesConcreteFactory : ProductAbstractFactory
    {
        public override Product MakeProduct(CreateProductCommand model)
        {
            Wipes wipesModel = new Wipes
            {
                Brand = model.Brand,
                CategoryID = model.CategoryID,
                Comments = model.Comments,
                DateCreated = DateTime.Now,
                IsActive = true,
                WipeQuantity = (int) model.WipeQuantity,
                Name = model.Name,
                PhotoLink = model.PhotoLink,
                Quantity = 0
            };
            return wipesModel;
        }
    }
}
