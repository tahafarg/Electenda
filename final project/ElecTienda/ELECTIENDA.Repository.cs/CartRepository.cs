using ELECTIENDA.Repository;
using ELECTIENDA.ViewModel;
using FirstLyer.Model;
using LinqKit;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;
using LinqKit;
using X.PagedList;


namespace ELECTIENDA.Repository
{

    public class CartRepository : GeneralRepository<Cart>
    {
        //private readonly FinalProjectContext Context;
        public CartRepository(FinalProjectContext _context) : base(_context)
        { }
        public List<CartViewModel> getForOrder(string UserId = "")
        {
            var filter = PredicateBuilder.New<Cart>();
            var oldfilter = filter;
            if (!string.IsNullOrEmpty(UserId))
                filter = filter.Or(p => p.UserID == UserId);

            if (oldfilter == filter)
                filter = null;

            var query = base.Get(filter).Where(i => i.IsDeleted == false);
            var result
                = query.Select(i => new CartViewModel
                {
                    ID = i.ID,
                    Name = i.Product.Name,
                    Price = i.Product.Price,
                    Quantity = i.Quantity,
                    UserID = i.UserID,
                    ServicesId = i.ServicesId,
                    ProductID = i.ProductID,

                    Src = i.Product.Images.FirstOrDefault().Src

                });
            return result.ToList();
        }

        public PaginingViewModel<List<CartViewModel>> get(int Id = 0, int? productId = 0, string UserId = "", string orderby = "id", bool isascending = false, int pageindex = 1, int pagesize = 10)
        {
            var filter = PredicateBuilder.New<Cart>();
            var oldfilter = filter;

            if (Id > 0)
                filter = filter.Or(p => p.ID == Id);
            if (!string.IsNullOrEmpty(UserId))
                filter = filter.Or(p => p.UserID == UserId);

            if (productId > 0)
                filter = filter.Or(p => p.ProductID == productId);


            if (oldfilter == filter)
                filter = null;

            var query = base.Get(filter, orderby, isascending, pageindex, pagesize, "Product", "Services");
            var result
                = query.Where(i => i.IsDeleted == false).Select(i => new CartViewModel
                {
                    ID = i.ID,
                    Name = i.Product.Name,
                    Price = i.Product.Price,
                    Quantity = i.Quantity,
                    UserID = i.UserID,
                    ServicesId = i.ServicesId,
                    ProductID = i.ProductID,

                    //Src = (i.ProductID != null) ? i.Product.Images.FirstOrDefault().Src : i.Services.Src

                });
            PaginingViewModel<List<CartViewModel>>
                finalresult = new PaginingViewModel<List<CartViewModel>>()
                {
                    PageIndex = pageindex,
                    PageSize = pagesize,
                    Count = base.GetList().Count(),
                    Data = result.ToList(),
                };


            return finalresult;
        }

        public IPagedList<CartViewModel> Search2(int Id = 0, string UserId = "",
            string orderby = "ID", bool isascending = false,
            int pageindex = 1, int pagesize = 10)
        {
            var filter = PredicateBuilder.New<Cart>();
            var oldfilter = filter;


            if (!string.IsNullOrEmpty(UserId))
                filter = filter.Or(p => p.UserID == UserId);

            if (oldfilter == filter)
                filter = null;

            var query = base.Get(filter, orderby, isascending, pageindex, pagesize, "Product", "Services");
            var result
                = query.Where(i => i.IsDeleted == false).Select(i => new CartViewModel
                {

                    ID = i.ID,
                    Name = i.Product.Name,
                    Price = i.Product.Price,
                    Quantity = i.Quantity,
                    UserID = i.UserID,
                    ServicesId = i.ServicesId,
                    ProductID = i.ProductID,
                }).ToPagedList(pageindex, pagesize);

            return result;
        }

        public CartViewModel? GetById(int id)
        {
            return base.GetList().Where(i => i.ID == id).Select(i => new CartViewModel
            {
                ID = i.ID,
                UserID = i.UserID,
                Name = i.Product.Name,
                Price = i.Product.Price,
                Quantity = i.Quantity,
                ServicesId = i.ServicesId,
                ProductID = i.ProductID,

            }).FirstOrDefault();

        }


        public CartViewModel Add(CartAddViewModel model)
        {
            Cart cart = model.ToModel();
            return base.Add(cart).Entity.ToViewModel();
        }

        public CartViewModel SoftDeleted(int id)
        {
            return base.isDeleted(id).Entity.ToViewModel();
        }



        public CartViewModel Update(CartViewModel model)
        {
            Cart cart = model.ToVModel();

            return base.Update(cart).Entity.ToViewModel();
        }

        public CartViewModel Remove(CartViewModel model)
        {
            Cart c = model.ToVModel();
            return base.Remove(c).Entity.ToViewModel();
        }


    }
}


