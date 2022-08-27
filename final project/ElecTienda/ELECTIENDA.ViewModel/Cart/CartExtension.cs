using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstLyer.Model;
using ELECTIENDA.ViewModel;

namespace ELECTIENDA.ViewModel
{
    public static class CartExtension
    {
        public static CartViewModel ToViewModel(this Cart model)
        {
            return new CartViewModel
            {
                ID = model.ID,
                UserID = model.UserID,
                ServicesId = model.ServicesId,
                ProductID = model.ProductID,

                Quantity = model.Quantity,
                Name = (model.Product != null) ? model.Product.Name : (model.Services != null)? model.Services.Name:"not provided Name",

                Price = (model.Product != null) ? model.Product.Price  : (model.Services != null) ? model.Services.Price : 0,
                //Src = model.Product?.Images?.FirstOrDefault()?.Src??"notfound.jpg"
            };
        }

        public static Cart ToModel(this CartAddViewModel model)
        {
            return new Cart
            {
                ID = model.ID,
                UserID = model.UserID,
                ProductID = model.ProductID,
                ServicesId = model.ServicesId,
                Quantity = model.Quantity,

            };

        }

        public static Cart ToVModel(this CartViewModel model)
        {
            return new Cart
            {
                ID = model.ID,
                UserID = model.UserID,
                ProductID = model.ProductID,
                ServicesId = model.ServicesId,
                Quantity = model.Quantity,

            };

        }



    }
}

