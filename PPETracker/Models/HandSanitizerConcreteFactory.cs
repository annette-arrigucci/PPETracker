using PPETracker.ViewModels;
using System;

namespace PPETracker.Models
{
    public class HandSanitizerConcreteFactory : ProductAbstractFactory
    {
        public override Product MakeProduct(CreateProductCommand model)
        {           
            HandSanitizer handSanitizerModel = new HandSanitizer
            {
                Brand = model.Brand,
                CategoryID = model.CategoryID,
                Comments = model.Comments,
                DateCreated = DateTime.Now,
                IsActive = true,
                SanitizerType = model.SanitizerType,
                NumOunces = (int)model.NumOunces,
                Name = model.Name,
                PhotoLink = model.PhotoLink,
                Quantity = 0
            };
            return handSanitizerModel;
        }
    }
}
