using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstLyer.Model;
using ELECTIENDA.ViewModel;

namespace ELECTIENDA.ViewModel
{
    public static class FavouriteExtension
    {
        public static FavoriteViewModel ToViewModel(this Favorite model)
        {
            return new FavoriteViewModel
            {
                ID = model.ID,
                UserID = model.UserID,
                ServicesId = model.ServicesId,
                ProductID = model.ProductID,
                Name= (model.Product != null) ? model.Product.Name : (model.Services != null) ? model.Services.Name : "not provided Name",
                //ProductName = model.Product.Name,
                //ServiceName = model.Services.Name,
                //ProductPrice = model.Product.Price,
                //ServicesPrice = model.Services.Price,
                
                 Price = (model.Product != null) ? model.Product.Price : (model.Services != null) ? model.Services.Price : 0,
                Img = model.Product?.Images?.FirstOrDefault()?.Src ?? "notfound.jpg"
            };
        }

        public static Favorite ToModel(this FavoriteEditViewModel model)
        {
            return new Favorite
            {
                ID = model.ID,
                UserID = model.UserID,
                ProductID = model.ProductID,
                ServicesId = model.ServicesId,
             
            };
        }

        public static Favorite ToVModel(this FavoriteViewModel model)
        {
            return new Favorite
            {
                ID = model.ID,
                UserID = model.UserID,
                ProductID = model.ProductID,
                ServicesId = model.ServicesId,


            };

        }
    }
}

