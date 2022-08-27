using FirstLyer.Model;

namespace ELECTIENDA.ViewModel
{
    public static class ExtensionOrder
    {

        public static Order ToModel(this OrderAddViewModel model)
        {
            return new Order
            {
                ID = model.ID,
                UserID = model.UserID,
                OrderDate = model.OrderDate,
                ShippingDate = model.ShippingDate,
                TotalPrice = model.TotalPrice,
                Statue = (FirstLyer.Model.statues)model.Statue,
                OrderDetailes = model.Details.Select(o => new OrderDetails
                {
                    ProductId = o.ProductId,
                    ServicesId = o.ServicesId,
                    Quantity = o.Quantity,
                    SubPrice = o.SupPrice

                }).ToList()
            };

        }

        public static OrderViewModel ToViewModel(this Order model)
        {
            return new OrderViewModel
            {
                // Images = model.BookImages?.Select(i => i.Image)?.ToList()
                ID = model.ID,
                UserName = (model.User != null)? model.User.FirstName+" "+ model.User.LastName:"",
                UserAddress=(model.User!=null)?model.User.Address:"",
                OrderDate = model.OrderDate,
                ShippingDate = model.ShippingDate,
                Statue = (statues)model.Statue,
                TotalPrice= model.TotalPrice,
                //ProductName=model.OrderDetailes.Select(o=>o.Product.Name).FirstOrDefault(),
                //Quntity= model.OrderDetailes.Select(o => o.Quantity).FirstOrDefault(),
                //ServicesName= model.OrderDetailes.Select(o => o.Services.Name).FirstOrDefault(),
                //SupPrice= model.OrderDetailes.Select(o => o.SubPrice).FirstOrDefault()
                Details = model.OrderDetailes.Select(o => new OrderDetailsViewModel()
                {
                    ProductName = (o.Product != null) ? o.Product.Name : "",
                    Service = (o.Services != null) ? o.Services.Name : "",
                    Quantity = o.Quantity,
                    SupPrice = o.SubPrice

                }).ToList()


            };
        }
    }
}
