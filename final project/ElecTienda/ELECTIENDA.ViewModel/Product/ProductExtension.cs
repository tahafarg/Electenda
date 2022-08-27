using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstLyer.Model;

namespace ELECTIENDA.ViewModel
{
    public static partial class ProductExtension
    {
        public static ProductViewModel ToViewModel(this Product model)
        {
            return new ProductViewModel
            {
                ID = model.ID,
                Name = model.Name,
                color = model.Color,
               // categoryName = model.Category.Name,
               // providerName = model.Provider.User.FirstName + "" + model.Provider.User.LastName,
                isActive = model.IsActive,
                Quantity = model.Quantity,
                price = model.Price,
                status = model.IsAccepted.ToString(),
                
                Imgs = model?.Images?.Select(i => i.Src)?.ToList()
            };
        }

        public static Product ToModel(this ProductEditViewModel model)
        {
            return new Product
            {
                ID = model.ID,
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                IsActive = model.isActive,
                Quantity = model.Quantity,
                Color = model.Color,
                ProviderID = model.ProviderID,
                CategoryID = model.CategoryID,
                //Status = productStatus.Pending,
                Images = model?.ImagesUrls?.Select(i => new Image
                {
                    Src = i
                }).ToList()

            };



        }
    }


}

