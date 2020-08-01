using PPETracker.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PPETracker.Models
{
    public class CanisterConcreteFactory : ProductAbstractFactory
    {
        public override Product MakeProduct(CreateProductCommand model)
        {
            //if the user did not choose a gas mask option from the list, assume they entered 
            //a gas mask name
            if (model.GasMaskAssociatedWith == "Other")
            {
                model.GasMaskAssociatedWith = model.UserEnteredGasMaskAssociatedWith;
            }
            if(model.GasMaskAssociatedWith == null)
            {
                model.GasMaskAssociatedWith = model.UserEnteredGasMaskAssociatedWith;
            }
            Canister canisterModel = new Canister
            {
                Brand = model.Brand,
                CategoryID = model.CategoryID,
                Comments = model.Comments,
                DateCreated = DateTime.Now,
                IsActive = true,
                CanisterType = model.CanisterType,
                GasMaskAssociatedWith = model.GasMaskAssociatedWith,
                Name = model.Name,
                PhotoLink = model.PhotoLink,
                Quantity = 0
            };
            return canisterModel;
        }
    }
}
