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
    public class RateRepository : GeneralRepository<Rate>
    {
        public RateRepository(FinalProjectContext _context) : base(_context)
        { }
        public PaginingViewModel<List<RateViewModel>> Get(decimal? rate = null, string ProductName = "",
                    string orderBy = "ID", bool isAscending = false, int pageIndex = 1,
                    int pageSize = 20)
        {
            var filter = PredicateBuilder.New<Rate>();
            var OldFilter = filter;

            if (rate != null)
                filter = filter.Or(i => i.Rating == rate);
            if (ProductName != null)
                filter = filter.Or(i => i.Product.Name.Contains(ProductName));

            if (OldFilter == filter)
                filter = null;
            var Query = base.Get(filter, orderBy, isAscending, pageIndex, pageSize);
            var Result
                = Query.Select(i => new RateViewModel
                {
                    ID = i.ID,
                    Rating = i.Rating,
                    Review = i.Review,

                    ProductName = i.Product.Name,
                    UserName = i.User.FirstName,
                    ServicesName = i.Services.Name

                });
            PaginingViewModel<List<RateViewModel>>
                finalResult = new PaginingViewModel<List<RateViewModel>>()
                {
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    Count = base.GetList().Count(),
                    Data = Result.ToList()
                };


            return finalResult;
        }

        public IPagedList<RateViewModel> Search2(decimal? rate = null, string ProductName = "",
                    string orderBy = "ID", bool isAscending = false, int pageIndex = 1,
                    int pageSize = 10)
        {
            var filter = PredicateBuilder.New<Rate>();
            var OldFilter = filter;

            if (rate != null)
                filter = filter.Or(i => i.Rating == rate);
            if (ProductName != null)
                filter = filter.Or(i => i.Product.Name.Contains(ProductName));

            if (OldFilter == filter)
                filter = null;
            var query = base.Get(filter, orderBy, isAscending, pageIndex, pageSize);
            var result
                = query.Where(i => i.IsDeleted == false).Select(i => new RateViewModel
                {
                    ID = i.ID,
                    Rating = i.Rating,
                    Review = i.Review,

                    ProductName = i.Product.Name,
                    UserName = i.User.FirstName,
                    ServicesName = i.Services.Name
                    //               phones = i.UserPhones.Select(x => x.Phone).ToList()
                }).ToPagedList(pageIndex, pageSize);

            return result;
        }

        public RateViewModel? GetByID(int _Id)
        {
            return base.GetList()
            .Where(i => i.ID == _Id).Select(i => new RateViewModel
            {
                ID = i.ID,
                Rating = i.Rating,
                Review = i.Review,

                UserName = i.User.FirstName,
                ProductName = i.Product.Name,
                ServicesName = i.Services.Name

            })?.FirstOrDefault();
        }


        public RateViewModel Add(RateEditViewModel model)
        {
            Rate rate = model.ToModel();
            return base.Add(rate).Entity.ToViewModel();

        }
        public RateViewModel RateAccepted(int _id)
        {
            return base.isAccepted(_id).Entity.ToViewModel();
        }

        public RateViewModel RateRejected(int _id)
        {
            return base.isRejected(_id).Entity.ToViewModel();
        }

    }
}
