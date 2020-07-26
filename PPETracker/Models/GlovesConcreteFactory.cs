using PPETracker.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PPETracker.Models
{
    public class GlovesConcreteFactory : ProductAbstractFactory
    {
        public override Product MakeProduct(CreateProductCommand model)
        {
            //if the user entered a new glove size type, make that the Glove Size entry
            if (model.GloveSize == "Other")
            {
                model.GloveSize = model.UserEnteredGloveSize;
            }

            Gloves glovesModel = new Gloves
            {
                Brand = model.Brand,
                CategoryID = model.CategoryID,
                Comments = model.Comments,
                DateCreated = DateTime.Now,
                IsActive = true,
                GloveQuantity = (int)model.GloveQuantity,
                GloveThickness = model.GloveThickness,
                GloveSize = model.GloveSize,
                Name = model.Name,
                PhotoLink = model.PhotoLink,
                Quantity = 0
            };
            return glovesModel;
        }
    }
}
