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
    public class ShopRepository : GeneralRepository<Shop>
    {

        public ShopRepository(FinalProjectContext _context) : base(_context)
        {

        }
        public PaginingViewModel<List<ShopViewModel>>
            Get(int ID = 0, int ProviderId = 0, string Name = ""
                              , string Address = "", string OrderBy = "ID"
                                , bool IsAscending = false, int pageIndex = 1, int pageSize = 5)
        {

            var filter = PredicateBuilder.New<Shop>();
            
            var OldFilter = filter;

            if (ID > 0)
                filter = filter.Or(p => p.ID == ID);
            if (ProviderId > 0)
                filter = filter.Or(p => p.ProviderId == ProviderId);
            if (!string.IsNullOrEmpty(Name))
                filter = filter.Or(p => p.Name.Contains(Name));
            if (!string.IsNullOrEmpty(Address))
                filter = filter.Or(p => p.Address.Contains(Address));
            

            if (OldFilter == filter)
                filter = null;


            var
                Query = base.Get(filter, OrderBy, IsAscending, pageIndex, pageSize);
            var result =
                Query.Where(i=>i.IsDeleted==false).Select(i => new ShopViewModel
                {
                    ID = i.ID,
                    Providerid=i.ProviderId,
                    //ProviderName = i.Provider.User.FirstName+" "+ i.Provider.User.LastName,
                    Name = i.Name,
                    Address = i.Address,
                    ImgSrc = i.ImgSrc,
                    status = i.IsAccepted.ToString()
                });

            PaginingViewModel<List<ShopViewModel>>
                finalResult = new PaginingViewModel<List<ShopViewModel>>()
                {
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    Count = base.GetList().Count(),
                    Data = result.ToList(),
                };


            return finalResult;


        }

        public IPagedList<ShopViewModel> Search(int ID = 0, int ProviderId = 0, string Name = ""
                              , string Address = "", string OrderBy = "ID"
                                , bool IsAscending = false, int pageIndex = 1, int pageSize = 5)
        {
            var filter = PredicateBuilder.New<Shop>();

            var OldFilter = filter;

            if (ID > 0)
                filter = filter.Or(p => p.ID == ID);
            if (ProviderId > 0)
                filter = filter.Or(p => p.ProviderId == ProviderId);
            if (!string.IsNullOrEmpty(Name))
                filter = filter.Or(p => p.Name.Contains(Name));
            if (!string.IsNullOrEmpty(Address))
                filter = filter.Or(p => p.Address.Contains(Address));
            

            if (OldFilter == filter)
                filter = null;


            var
                Query = base.Get(filter, OrderBy, IsAscending, pageIndex, pageSize);
            var result =
                Query.Where(i => i.IsDeleted == false).Select(i => new ShopViewModel
                {
                    ID = i.ID,
                    Providerid = i.ProviderId,
                    //ProviderName = i.Provider.User.FirstName+" "+ i.Provider.User.LastName,
                    Name = i.Name,
                    Address = i.Address,
                    ImgSrc = i.ImgSrc,
                    status = i.IsAccepted.ToString()
                }).ToPagedList(pageIndex, pageSize);
            return result;
        }

        public ShopEditViewModel? GetByID(int _Id)
        {
            return base.GetList().Where(i => i.ID == _Id).Select(i => new ShopEditViewModel
            {
                Id = i.ID,
                Name = i.Name,
                Address=i.Address,
                
                ProviderId = i.ProviderId,
            }).FirstOrDefault();

        }

        public ShopViewModel add(ShopEditViewModel model)
        {
            Shop shop = model.ToModel();
            return base.Add(shop).Entity.ToViewModel();
        }

        public ShopViewModel Update(ShopEditViewModel model)
        {
            Shop shop = model.ToModel();
            return base.Update(shop).Entity.ToViewModel();
        }

        public ShopViewModel ShopAccepted(int _id)
        {
            return base.isAccepted(_id).Entity.ToViewModel();
        }

        public ShopViewModel ShopRejected(int _id)
        {
            return base.isRejected(_id).Entity.ToViewModel();
        }

        public ShopViewModel SoftDeleted(int id)
        {
            return base.isDeleted(id).Entity.ToViewModel();
        }


    }
}