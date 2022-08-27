using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstLyer.Model;
using ELECTIENDA.ViewModel;
using LinqKit;
using X.PagedList;

namespace ELECTIENDA.Repository
{
    public class OrderDetailsRepository : GeneralRepository<OrderDetails>
    {
        public OrderDetailsRepository(FinalProjectContext _dbContext) : base(_dbContext)
        { }
        public IPagedList<OrderDetailsViewModel> GetProviderOrder(int ProviderId, int pageIndex = 1, int pageSize = 1)
        {
         return base.GetList().Where(o => o.Product.ProviderID == ProviderId).Select(o => new OrderDetailsViewModel
            {
                ID = o.Order.ID,
                OrderDate = o.Order.OrderDate,
                ShippingDate = o.Order.ShippingDate,
                Statue = (ELECTIENDA.ViewModel.statues)o.Order.Statue,
                ProductName=o.Product.Name,
                Quantity=o.Quantity,
                UserName=o.Order.User.FirstName+" "+o.Order.User.LastName,
                UserAddress= o.Order.User.Address,
                TotalPrice=o.Order.TotalPrice,
                SupPrice=o.SubPrice

            }).ToPagedList(pageIndex,pageSize);
        }
        public PaginingViewModel<List<OrderDetailsViewModel>> GetOrderDetails(int id = 0, float TotalPrice = 0, string orderBy = null, bool isAscending = false, int pageIndex = 1, int pageSize = 20)
        {
            var filter = PredicateBuilder.New<OrderDetails>();
            var oldFilter = filter;
            if (id > 0)
                filter = filter.Or(i => i.ID == id);
            if (TotalPrice > 0)
                filter = filter.Or(i => i.SubPrice == TotalPrice);
            if (filter == oldFilter)
                filter = null;

            var query = base.Get(filter, orderBy, isAscending, pageIndex, pageSize, null);

            var Result =
                query.Select(i => new OrderDetailsViewModel
                {
                    ID = i.ID,
                    ProductName = i.Product.Name,
                    Service = i.Services.Name,
                    Quantity = i.Quantity,
                    SupPrice = i.SubPrice,

                });

            PaginingViewModel<List<OrderDetailsViewModel>>
                finalResult = new PaginingViewModel<List<OrderDetailsViewModel>>()
                {
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    Count = GetList().Count(),
                    Data = Result.ToList()
                };

            return finalResult;
        }

        public OrderDetailsViewModel Add(OrderDetailsAddViewModel model)
        {
            OrderDetails details = model.ToModel();
            return base.Add(details).Entity.ToViewModel();
        }
    }
}
