using FirstLyer.Model;
using ELECTIENDA.ViewModel;
using LinqKit;
using X.PagedList;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using System.Security.Claims;

namespace ELECTIENDA.Repository
{
    public class OrderRepository : GeneralRepository<Order>
    {
        public OrderRepository(FinalProjectContext _context) : base(_context)
        {

        }
        public PaginingViewModel<List<OrderViewModel>> Get( DateTime? OrderDate = null, DateTime? ShipingDate = null ,
                   string orderBy = "ID", bool isAscending = false, int pageIndex = 1,
                   int pageSize = 10 )
        {

            var filter = PredicateBuilder.New<Order>();
            var OldFilter = filter;

            if (OrderDate != null)
                filter = filter.Or(i => i.OrderDate == OrderDate);
            if (ShipingDate != null)
                filter = filter.Or(i => i.ShippingDate == ShipingDate);

            if (OldFilter == filter)
                filter = null;

            var Query = base.Get( filter , orderBy, isAscending, pageIndex, pageSize);
            var Result
                = Query.Select(o => new OrderViewModel()
                {
                    ID = o.ID,
                    UserName = o.User.FirstName + o.User.LastName,
                    OrderDate = o.OrderDate,
                    ShippingDate = o.ShippingDate,
                    ////       Amount = o.Amount,
                    //       Statue = (ELECTIENDA.ViewModel.statues)o.Statue,
                    //       Product=o.OrderDetails.Select(o=>o.Product.Name).ToString(),
                    //       Service=o.OrderDetails.Select(o=>o.Services.Name).ToString(),
                    //       Price = o.OrderDetails.Select(o=>o.TotalPrice).ToString(),
                    //       Quntity=o.OrderDetails.Select(o=>o.Quantity).ToString()


                   //Details = o.OrderDetailes.Select(o => new OrderDetailsViewModel()
                   // {
                   //     Product = o.Product.Name,
                   //     Service = o.Services.Name,
                   //     Quantity = o.Quantity,
                   //     SupPrice = o.SubPrice

                   // }).ToList()
                } ) ;

            PaginingViewModel<List<OrderViewModel>>
                finalResult = new PaginingViewModel<List<OrderViewModel>>()
                {
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    Count = GetList().Count(),
                    Data = Result.ToList()
                };

            return finalResult;
        }


        public IPagedList<OrderViewModel> Search2( DateTime orderDate, DateTime shippingDate, string orderBy = "ID", bool isAscending = false
            , int pageIndex = 1, int pageSize = 10)
        {
            var filter = PredicateBuilder.New<Order>();
            var OldFilter = filter;
            if (orderDate != null)
                filter = filter.Or(i => i.OrderDate == orderDate);
            if (shippingDate != null)
                filter = filter.Or(i => i.ShippingDate == shippingDate);
      


            if (OldFilter == filter)
                filter = null;
            var query = base.Get(filter, orderBy, isAscending, pageIndex, pageSize);
            var result
                = query.Where(i => i.IsDeleted == false).Select(i => new OrderViewModel
                {

                }).ToPagedList(pageIndex, pageSize);
            return result;
        }

        public IPagedList<OrderViewModel> Search(int pageIndex = 1, int pageSize = 1)
        {
            return GetList().Select(o => new OrderViewModel
            {
                ID = o.ID,
                UserName = o.User.FirstName + o.User.LastName,
                OrderDate = o.OrderDate,
                ShippingDate = o.ShippingDate,
                TotalPrice= o.TotalPrice,
                UserAddress=o.User.Address,
                //       Amount = o.Amount,            
                Statue = (ELECTIENDA.ViewModel.statues)o.Statue,

                //Product = o.OrderDetails.Select(o => o.Product.Name).ToString(),
                //Service = o.OrderDetails.Select(o => o.Services.Name).ToString(),
                //Price = o.OrderDetails.Select(o => o.TotalPrice).ToString(),
                //Quntity = o.OrderDetails.Select(o => o.Quantity).ToString(),

                Details = o.OrderDetailes.Select(o => new OrderDetailsViewModel()
                {
                    ProductName = o.Product.Name,
                    Service = o.Services.Name,
                    Quantity = o.Quantity,
                    SupPrice = o.SubPrice

                }).ToList()


            }).ToPagedList(pageIndex, pageSize);
        }
        public OrderViewModel? GetById(int id)
        {
            return
            base.GetList().Where(o => o.ID == id).Select(o => new OrderViewModel
            {
                ID = o.ID,
                UserName = o.User.FirstName + o.User.LastName,
                OrderDate = o.OrderDate,
                ShippingDate = o.ShippingDate,
                //       Amount = o.Amount,
                Statue = (ELECTIENDA.ViewModel.statues)o.Statue,
                //SupPrice=o.OrderDetailes.Select(o=>o.SubPrice).FirstOrDefault(),
                //ProductName = o.OrderDetailes.Select(o=>o.Product.Name).FirstOrDefault(),
                //Quntity = o.OrderDetailes.Select(o=>o.Quantity).FirstOrDefault(),
                //ServicesName=o.OrderDetailes.Select(o=>o.Services.Name).FirstOrDefault()

                //Details = o.OrderDetailes.Select(o => new OrderDetailsViewModel()
                //{
                //    Product = o.Product.Name,
                //    Service = o.Services.Name,
                //    Quantity = o.Quantity,
                //    SupPrice = o.SubPrice

                //}).ToList()

            }).FirstOrDefault();
        }
        public OrderViewModel? GetByIdForOrderDetailes(int id)
        {
            return
            base.GetList().Where(o =>o.OrderDetailes.All(o=>o.OrderId==id)).Select(o => new OrderViewModel
            {
                ID = o.ID,
                UserName = o.User.FirstName + o.User.LastName,
                OrderDate = o.OrderDate,
                ShippingDate = o.ShippingDate,
                //       Amount = o.Amount,
                Statue = (ELECTIENDA.ViewModel.statues)o.Statue,
                //SupPrice=o.OrderDetailes.Select(o=>o.SubPrice).FirstOrDefault(),
                //ProductName = o.OrderDetailes.Select(o=>o.Product.Name).FirstOrDefault(),
                //Quntity = o.OrderDetailes.Select(o=>o.Quantity).FirstOrDefault(),
                //ServicesName=o.OrderDetailes.Select(o=>o.Services.Name).FirstOrDefault()

                Details = o.OrderDetailes.Select(o => new OrderDetailsViewModel()
                {
                    ProductName = o.Product.Name,
                    Service = o.Services.Name,
                    Quantity = o.Quantity,
                    SupPrice = o.SubPrice

                    }).ToList()

                }).FirstOrDefault();
        }
        public OrderViewModel Add(OrderAddViewModel model)
        {
            Order order = model.ToModel();
            return base.Add(order).Entity.ToViewModel();
        }


        public OrderViewModel? Update(int id , OrderAddViewModel addModel)
        {
       
           
            return base.Update(addModel.ToModel()).Entity.ToViewModel();

        }


        public OrderViewModel Cancele(int id)
        {
            Order order = GetList().Where(o => o.ID == id).FirstOrDefault();
            order.Statue = FirstLyer.Model.statues.Canceled;
            return base.Update(order).Entity.ToViewModel();
        }

        public OrderViewModel Done(int id)
        {
            
            Order order = GetList().Where(o => o.ID == id).FirstOrDefault();
            order.Statue = FirstLyer.Model.statues.Done;
            return base.Update(order).Entity.ToViewModel();
        }

        //public List<OrderViewModel> getForOrder(Order_Id)
        //{
        //    var filter = PredicateBuilder.New<Order>();
        //    var oldfilter = filter;
        //    if ()
        //        filter = filter.Or(p => p.UserID == UserId);

        //    if (oldfilter == filter)
        //        filter = null;

        //    var query = base.GetList().Where(c => c.UserID == UserId);
        //    var result
        //        = query.Where(i => i.IsDeleted == false).Select(i => new CartViewModel
        //        {
        //            ID = i.ID,
        //            Name = (i.ProductID != null) ? i.Product.Name : i.Services.Name,
        //            Price = (i.Product != null) ? i.Product.Price : i.Services.Price,
        //            Quantity = i.Quantity,
        //            UserID = i.UserID,
        //            ServicesId = i.ServicesId,
        //            ProductID = i.ProductID,

        //            Src = (i.ProductID != null) ? i.Product.Images.FirstOrDefault().Src : i.Services.Src

        //        });



        //    return result.ToList();
        //}


        //public void Balance (int User_Id)
        //{
        //    //User_Id = Us(ClaimTypes.NameIdentifier);
        //    User user = new User();
        //    //user.Provider.ProviderID = User_Id;
            
        //    List<Order> order = GetList().ToList();
        //    foreach (var  o in order)
               

        //}


    }
}
