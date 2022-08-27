using FirstLyer.Model;

namespace ELECTIENDA.ViewModel
{
    public static class ExtensionOrderDetails
    {

        public static OrderDetails ToModel(this OrderDetailsAddViewModel model)
        {
            return new OrderDetails
            {
                ID = model.ID,
                OrderId = model.OrderID,
                SubPrice = model.SupPrice,
                ProductId = model.ProductId,
                ServicesId = model.ServicesId,
                Quantity = model.Quantity,
            };

        }

        public static OrderDetailsViewModel ToViewModel(this OrderDetails model)
        {
            return new OrderDetailsViewModel
            {
                ID = model.ID,
                OrderID= model.OrderId,
                SupPrice = model.SubPrice,
                ProductName = model.Product.Name,
                Service = model.Services.Name,
                Quantity = model.Quantity,
            };
        }
    }
}
