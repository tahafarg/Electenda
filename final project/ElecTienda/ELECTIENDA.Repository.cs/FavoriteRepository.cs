using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ELECTIENDA.ViewModel;
using FirstLyer.Model;
using LinqKit;
using X.PagedList;


namespace ELECTIENDA.Repository
{
    public class FavoriteRepository : GeneralRepository<Favorite>
    {
        public FavoriteRepository(FinalProjectContext _context) : base(_context)
        {
        }
        public PaginingViewModel<List<FavoriteViewModel>> Get(int ID = 0, int ServicesId = 0, string UserId = "", string ServicesName = "",
             int ProductId = 0, string ProductName = "", int Quantity = 0,
            string orderby = "ID", bool IsDeleted = false, bool isAscending = false, int pageIndex = 1,
            int pageSize = 20)
        {
            var query = PredicateBuilder.New<Favorite>();
            if (ID > 0)
                query = query.Or(p => p.ID == ID);
            if (ServicesId > 0)
                query = query.Or(p => p.ServicesId == ServicesId);
            if (UserId != null)
                query = query.Or(p => p.UserID == UserId);
            if (ProductId > 0)
                query = query.Or(P => P.ProductID == ProductId);
            if (ProductName != null)
                query = query.Or(p => p.Product.Name.Contains(ProductName));
            if (ServicesName != null)
                query = query.Or(p => p.Product.Name.Contains(ServicesName));

            var
            filter = base.Get(query, orderby, isAscending, pageIndex, pageSize);
            var result =
            filter.Select(i => new FavoriteViewModel
            {
                ID = i.ID,
                UserID = i.UserID,
                ServicesId = i.ServicesId,
                ProductID = i.ProductID,
                //Name = (i.Product != null) ? i.Product.Name : (i.Services != null) ? i.Services.Name : "not provided Name",
                //Price = (i.Product != null) ? i.Product.Price : (i.Services != null) ? i.Services.Price : 0,
                //ProductName = i.Product.Name,
                //ServiceName = i.Services.Name,
                //ProductPrice = i.Product.Price,
                //ServicesPrice = i.Services.Price,
                Name = i.Product.Name,
                Price = i.Product.Price,
                Img = i.Product.Images.FirstOrDefault().Src

            });

            PaginingViewModel<List<FavoriteViewModel>>
                finalResult = new PaginingViewModel<List<FavoriteViewModel>>()
                {
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    Count = base.GetList().Count(),
                    Data = result.ToList()
                };
            return finalResult;

        }


        public FavoriteViewModel GetbyID(int _Id)
        {
            return base.GetList().Where(i => i.ID == _Id).Select(i => new FavoriteViewModel
            {

                ID = i.ID,
                UserID = i.UserID,
                //Name = (i.Product != null) ? i.Product.Name : (i.Services != null) ? i.Services.Name : "not provided Name",
                //Price = (i.Product != null) ? i.Product.Price : (i.Services != null) ? i.Services.Price : 0,
                Name = i.Product.Name,
                Price = i.Product.Price,
                //ProductName = i.Product.Name,
                //ServiceName = i.Services.Name,
                //ProductPrice=i.Product.Price,
                //ServicesPrice=i.Services.Price,
                Img = i.Product.Images.FirstOrDefault().Src
            }).FirstOrDefault();

        }

        //public IPagedList<FavoriteViewModel> Search(int PageIndex = 2, int PageSize = 1) =>

        //    GetList().Select(i => new FavoriteViewModel
        //    {
        //        ID = i.ID,
        //        UserID = i.UserID,
        //        ServiceName = i.Services.Name,
        //        ProductName = i.Product.Name,
        //        Price = (i.Product != null) ? i.Product.Price : (i.Services != null) ? i.Services.Price : 0,
        //        Img = i.Product.Images.FirstOrDefault().Src
        //    }).ToPagedList(PageIndex, PageSize);

        public FavoriteViewModel Add(FavoriteEditViewModel model)
        {
            Favorite Favorite = model.ToModel();
            return base.Add(Favorite).Entity.ToViewModel();
        }

        public FavoriteViewModel Deleted(int id)
        {
            return base.isDeleted(id).Entity.ToViewModel();
        }

        public FavoriteViewModel Update(FavoriteEditViewModel model)
        {
            Favorite Fav = model.ToModel();
            return base.Update(Fav).Entity.ToViewModel();
        }


        public List<FavoriteViewModel> getForOrder(string UserId = "")
        {
            var filter = PredicateBuilder.New<Favorite>();
            var oldfilter = filter;
            if (!string.IsNullOrEmpty(UserId))
                filter = filter.Or(p => p.UserID == UserId);

            if (oldfilter == filter)
                filter = null;

            var query = base.Get(filter).Where(i => i.IsDeleted == false);
            var result
                = query.Select(i => new FavoriteViewModel
                {
                    ID = i.ID,
                    UserID = i.UserID,
                    //Name = (i.Product != null) ? i.Product.Name : (i.Services != null) ? i.Services.Name : "not provided Name",
                    //Price = (i.Product != null) ? i.Product.Price : (i.Services != null) ? i.Services.Price : 0,
                    Name = i.Product.Name,
                    Price = i.Product.Price,
                    //ProductName = i.Product.Name,
                    //ServiceName = i.Services.Name,
                    //ProductPrice = i.Product.Price,
                    //ServicesPrice = i.Services.Price,
                    ServicesId = i.ServicesId,
                    ProductID = i.ProductID,


                     Img = i.Product.Images.FirstOrDefault().Src

                });

            return result.ToList();
        }


        public FavoriteViewModel Remove(FavoriteViewModel model)
        {
            Favorite f = model.ToVModel();
            return base.Remove(f).Entity.ToViewModel();
        }

    }
}
