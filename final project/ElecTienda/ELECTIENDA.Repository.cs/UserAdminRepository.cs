using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstLyer.Model;
using LinqKit;
using ELECTIENDA.ViewModel;
using X.PagedList;
using Microsoft.AspNetCore.Identity;

namespace ELECTIENDA.Repository
{
    public class UserAdminRepository : GeneralRepository<User>
    {
        FinalProjectContext context;
        public UserAdminRepository(FinalProjectContext _context) : base(_context)
        {
            context = _context; 
        }
        public PaginingViewModel<List<UserViewModel>> Get(string FirstName = "",

                    string LastName = "",
                    string Address = "",
                    string phone = "",
                    string orderBy = "ID", bool isAscending = false, int pageIndex = 1,
                    int pageSize = 20)
        {
            var filter = PredicateBuilder.New<User>();
            var OldFilter = filter;
            if (!string.IsNullOrEmpty(FirstName))
                filter = filter.Or(i => i.FirstName.Contains(FirstName));
            if (!string.IsNullOrEmpty(LastName))
                filter = filter.Or(i => i.LastName.Contains(LastName));
            if (!string.IsNullOrEmpty(Address))
                filter = filter.Or(i => i.Address.Contains(Address));
            if (!string.IsNullOrEmpty(phone))
                filter = filter.Or(p => p.UserPhones.Any(i => i.Phone.Contains(phone)));
            if (OldFilter == filter)
                filter = null;
            var Query = base.Get(filter, orderBy, isAscending, pageIndex, pageSize, "UserPhones");
            var Result
                = Query.Select(i => new UserViewModel
                {

                    ID = i.Id ,
                    FirstName = i.FirstName,
                    LastName = i.LastName,
                    Address = i.Address,
                    //ImgSrc = i.ImgSrc,
                    //  phones = i.UserPhones.Select(x => x.Phone).ToList()
                });
            PaginingViewModel<List<UserViewModel>>
                finalResult = new PaginingViewModel<List<UserViewModel>>()
                {
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    Count = base.GetList().Count(),
                    Data = Result.ToList()
                };


            return finalResult;
        }

        public UserViewModel? GetByID(string _Id)
        {
            return base.GetList()
            .Where(i => i.Id == _Id).Select(i => new UserViewModel
            {

                FirstName = i.FirstName,
                LastName = i.LastName,
                Address = i.Address,
                //ImgSrc = i.ImgSrc,
        //        phones = i.UserPhones.Select(x => x.Phone).ToList()
            })?.FirstOrDefault();
        }

        public IPagedList<UserViewModel> Search2(string FirstName = "",

                    string LastName = "",
                    string Address = "",
                    int? ProviderId= null,
                  
                    string orderBy = "ID", bool isAscending = false , int pageIndex = 1,
                    int pageSize = 10)      
        {
            var filter = PredicateBuilder.New<User>();
            var OldFilter = filter;
            if (!string.IsNullOrEmpty(FirstName))
                filter = filter.Or(i => i.FirstName.Contains(FirstName));
            if (!string.IsNullOrEmpty(LastName))
                filter = filter.Or(i => i.LastName.Contains(LastName));
            if (!string.IsNullOrEmpty(Address))
                filter = filter.Or(i => i.Address.Contains(Address));
            if (ProviderId != null)
                filter = filter.Or(i => i.Provider.ProviderID == ProviderId);
            //if (!string.IsNullOrEmpty(phone))
            //    filter = filter.Or(p => p.UserPhones.Any(i => i.Phone.Contains(phone)));
            if (OldFilter == filter)
                filter = null;
            var query = base.Get(filter, orderBy, isAscending, pageIndex, pageSize);
            var result
                = query.Where(i=>i.IsDeleted == false).Select(i => new UserViewModel
                {
                    ID = i.Id ,
                    FirstName = i.FirstName,
                    LastName = i.LastName,
                    Address = i.Address,
                    ImgSrc = i.ImgSrc,
                    ProviderId=i.Provider.ProviderID
                    
     //               phones = i.UserPhones.Select(x => x.Phone).ToList()
                }).ToPagedList(pageIndex, pageSize);

            return result ;
            }

        public IPagedList<UserViewModel> Search(int pageIndex = 1, int pageSize = 1)
        => GetList().Select(u => new UserViewModel
        {
             ID = u.Id,
            FirstName = u.FirstName,
            LastName = u.LastName,
            Address = u.Address,
            //ImgSrc = u.ImgSrc,
   //         phones = u.UserPhones.Select(x => x.Phone).ToList()
        }).ToPagedList(pageIndex, pageSize);

        public UserViewModel Remove(string ID)
        {
            //        User user = context.Users.FirstOrDefault(x => x.Id == id);
            User user = base.GetList().Where(i => i.Id == ID).FirstOrDefault();
            user.IsDeleted = true;
            return base.Update(user).Entity.ToViewModel();

        }




    }
}


