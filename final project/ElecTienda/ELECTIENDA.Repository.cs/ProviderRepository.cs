using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstLyer.Model;
using LinqKit;
using ELECTIENDA.ViewModel;
using X.PagedList;
using X.PagedList.Mvc.Core;
using Microsoft.AspNetCore.Identity;
using ELECTIENDA.Repository;

namespace ELECTIENDA.Repository
{
    public class ProviderRepository : GeneralRepository<Provider>
    {
        UserManager<User> userManager;
        ProductRepository Product;
        ServicesRepository Servicesrepo;
        SignInManager<User> SignInManager;
        public ProviderRepository(FinalProjectContext _dbContext, UserManager<User> _userManager,SignInManager<User> signInManager, ProductRepository product,ServicesRepository services) : base(_dbContext)
        {
            userManager = _userManager;
            Product = product;
            Servicesrepo = services;
            SignInManager = signInManager;
        }
        public PaginingViewModel<List<ProviderViewModel>> GetProviders(int id = 0, string name = "", string phone = "", string orderby = "ProviderID", bool isAscending = false, int pageIndex = 1, int pageSize = 20)
        {
            var filter = PredicateBuilder.New<Provider>();
            var oldFiler = filter;
            if (id > 0)
                filter = filter.Or(p => p.ProviderID == id);
            if (!string.IsNullOrEmpty(name))
                filter = filter.Or(p => p.User.FirstName.Contains(name));
            if (!string.IsNullOrEmpty(phone))
                filter = filter.Or(p => p.User.UserPhones.Any(u => u.Phone.Contains(phone)));

            if (filter == oldFiler)
                filter = null;
            var query = base.Get(filter, orderby, isAscending, pageIndex, pageSize, null);

            var Result =
            query.Select(i => new ProviderViewModel
            {
                ID = i.ProviderID,
                Name = i.User.FirstName + "" + i.User.LastName,
               
                licenceImg = i.LicenseImg,
                Phones = i.User.UserPhones.Select(up => up.Phone).ToList(),
                shops = i.Shops.Select(sh => sh.Name).ToList()

            });

            PaginingViewModel<List<ProviderViewModel>>
                   finalResult = new PaginingViewModel<List<ProviderViewModel>>()
                   {
                       PageIndex = pageIndex,
                       PageSize = pageSize,
                       Count = base.GetList().Count(),
                       Data = Result.ToList()
                   };


            return finalResult;
        }



       public IPagedList<ProviderViewModel> Search( int id =0,string name = "", string orderBy = "", bool isAscending = false, int pageIndex = 1, int pageSize = 20) 
        { 
            var filter = PredicateBuilder.New<Provider>(); 
            var oldFilter = filter; 
            if(id > 0) 
                filter = filter.Or(i => i.ProviderID == id); 
            if (!string.IsNullOrEmpty(name)) 
                filter = filter.Or(i => i.User.FirstName.Contains(name) || i.User.LastName.Contains(name)); 
            if (filter == oldFilter) 
                filter = null; 
 
            var data = base.Get(filter, orderBy, isAscending, pageIndex, pageSize, null); 
 
            var paginatedProviders = data.Where(p => p.IsDeleted == false).Select(i => new ProviderViewModel 
            { 
                ID = i.ProviderID, 
                Name = i.User.FirstName + "" + i.User.LastName, 
                licenceImg = i.LicenseImg, 
                Img = i.User.ImgSrc,
                Phones = i.User.UserPhones.Select(up => up.Phone).ToList(), 
                shops = i.Shops.Select(sh => sh.Name).ToList(), 
                role = i.Membership.Type 
            }).ToPagedList(pageIndex, pageSize); 
 
            return paginatedProviders; 
        } 
 
        public ProviderViewModel Remove(int _id) 
        { 
            Provider data = base.GetList().Where(i => i.ProviderID == _id).FirstOrDefault(); 
            data.IsDeleted = true;
            return base.Update(data).Entity.ToViewModel(); 
 
        }

        //public ProviderViewModel Delete(int _id)
        //{
        //    var data = base.GetList().Where(i => i.ProviderID == _id).FirstOrDefault();
        //    //return base.Delete(data).Entity.ToViewModel();

        //}

        public async Task<IdentityResult> SignUp(ProviderAddViewModel model)
        {
            User user = model.ToModel();
            var Result = await userManager.CreateAsync(user, model.Password);
            user.Provider = model.ToProvModel();

            if (Result.Succeeded)
            {
                base.Add(user.Provider);
                Result = await userManager.AddToRoleAsync(user, "Provider");

            }
            return Result;

        }
        public async Task<SignInResult> SignIn(UserLogInViewModel model) => 
            await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, true);
        //public void Balance(Provider provider)
        //{
        //    var arr= provider.Products;
        //    float Sum = 0;
        //    foreach (var Product in arr) { 
        //        var Data = Product.OrderDetailes;
        //        foreach(var Result in Data)
        //        {
        //            Sum += Result.SubPrice;
        //        }
        //    }
        //    provider.Balance = Sum;
        //}


        public ProviderViewModel UpdateBalance(int Id, float ProviderBalance)
        {
            Provider data = base.GetList().Where(i => i.ProviderID == Id).FirstOrDefault();
            data.Balance += ProviderBalance - (10/100) * ProviderBalance;
            return base.Update(data).Entity.ToViewModel();


        }

        public void Balance (int? Product_id,int? Services_id , float SupPrice)
        {
            if(Product_id >0)
            { 
            var Result= Product.GetByIdForBalance(Product_id);
            int Id= Result.ProviderID;
                UpdateBalance(Id, SupPrice);
                
            }
            if(Services_id>0)
            {
              var Result = Servicesrepo.GetByIDForBalance(Services_id);
              int Id = Result.ProviderId;
                UpdateBalance(Id, SupPrice);
            }

        }
        public ProviderViewModel getproviderbyUserID(string Id)
        {
            Provider provider = base.GetList().Where(u => u.UserID == Id).FirstOrDefault();
            return new ProviderViewModel
            {
                ID = provider.ProviderID,
                Name = provider.User.FirstName + " " + provider.User.LastName,
                licenceImg = provider.LicenseImg,
                Balance=provider.Balance,
               // Img = provider.User.ImgSrc,



            };
        }

        public ServicesDetailsAPIViewModel GetServicesDetailsAPI(int id)
        {
            return
             base.GetList().Where(p => p.ProviderID == id).Select(p => new ServicesDetailsAPIViewModel
             {
                 ProviderName = p.User.FirstName + " " + p.User.LastName,
                 ProviderAddress = p.User.Address,
                 Shops = p.Shops.Select(sh => sh.Name + "  " + sh.Address).ToList(),

             }).FirstOrDefault();


        }

    }
}
