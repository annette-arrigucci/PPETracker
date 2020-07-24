using PPETracker.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PPETracker.Models
{
    public class MaskConcreteFactory : ProductAbstractFactory
    {
        public override Product MakeProduct(CreateProductCommand model)
        {
            Mask maskModel = new Mask
            {
                Brand = model.Brand,
                CategoryID = model.CategoryID,
                Comments = model.Comments,
                DateCreated = DateTime.Now,
                IsActive = true,
                MaskType = model.MaskType,
                Name = model.Name,
                PhotoLink = model.PhotoLink,
                Quantity = 0
            };
            return maskModel;
        }
    }
}
