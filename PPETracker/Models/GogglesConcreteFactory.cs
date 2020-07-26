using PPETracker.ViewModels;
using System;

namespace PPETracker.Models
{
    public class GogglesConcreteFactory : ProductAbstractFactory
    {
        public override Product MakeProduct(CreateProductCommand model)
        {
            Goggles gogglesModel = new Goggles
            {
                Brand = model.Brand,
                CategoryID = model.CategoryID,
                Comments = model.Comments,
                DateCreated = DateTime.Now,
                IsActive = true,
                GogglesType = model.GogglesType,
                Name = model.Name,
                PhotoLink = model.PhotoLink,
                Quantity = 0
            };
            return gogglesModel;
        }
    }
}
