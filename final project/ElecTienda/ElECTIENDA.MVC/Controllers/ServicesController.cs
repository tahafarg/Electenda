using ELECTIENDA.Repository;
using ELECTIENDA.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ElECTIENDA.MVC
{

    public class ServicesController : Controller
    {
        private readonly ServicesRepository ServicesRepo;
        private readonly CategoryRepository CatRepo;
        private readonly UnitOfWork unitOfWork;
        private readonly UserRepository UserRepository;
        private readonly ProviderRepository provider;
        public ServicesController(UserRepository user,ProviderRepository repository,ServicesRepository _ServicesRepo, UnitOfWork _UnitOfWork, CategoryRepository _CatRepo)
        {
            ServicesRepo = _ServicesRepo;
            unitOfWork = _UnitOfWork;
            CatRepo = _CatRepo;
            UserRepository = user;
            provider = repository;
        }

        public IActionResult Get(string name = "",int ProviderId =0, float price = 0.00f,
            string orderby = "ProviderID", bool isAscending = false,
            int pageIndex = 1, int pageSize = 20)
        {
            //    PaginingViewModel<List<ServicesViewModel>> result
            //        = ServicesRepo.Get(name, price, orderby, isAscending, pageIndex,
            //                     pageSize);
            //    return View(result.Data);


            var result = ServicesRepo.Search2(name,ProviderId, price, orderby, isAscending, pageIndex,
                             pageSize);
            return View(result);
        }

        public IActionResult ProviderGet(string name = "",int ProviderId =0, float price = 0.00f,
            string orderby = "ProviderID", bool isAscending = false,
            int pageIndex = 1, int pageSize = 20)
        {
            //PaginingViewModel<List<ServicesViewModel>> result
            //    = ServicesRepo.Get(name, price, orderby, isAscending, pageIndex,
            //                 pageSize);
            //return View(result.Data);

            var clamisidentity = (System.Security.Claims.ClaimsIdentity)this.User.Identity;
            var claim = clamisidentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            var UserId = claim.Value;
            ViewBag.Provider = provider.getproviderbyUserID(UserId);
            var result = ServicesRepo.Search2(name,ProviderId, price, orderby, isAscending, pageIndex,
                             pageSize);
            return View(result);
        }

        //[Route "Api"]
        public ResultViewModel GetApi(string name = "", float price = 0.00f,
            string orderby = "ProviderID", bool isAscending = false,
            int pageIndex = 1, int pageSize = 20)
        {
            PaginingViewModel<List<ServicesViewModel>> result
                = ServicesRepo.Get(name, price, orderby, isAscending, pageIndex,
                             pageSize);
            return new ResultViewModel
            {
                Success = true ,
                Message ="",
                Data = result
                  
            };
        }

        [HttpGet]
        public IActionResult Add()
        {
            var clamisidentity = (System.Security.Claims.ClaimsIdentity)this.User.Identity;
            var claim = clamisidentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            var userid = claim.Value;
            int provider_Id = UserRepository.Detailes(userid);
            ViewBag.Provider = provider.getproviderbyUserID(userid);
            ViewBag.Categories = GetCategoryesDropDownList();
            return View(new ServicesAddViewModel { ProviderId =provider_Id});
        }
        [HttpPost]
        public IActionResult Add( ServicesAddViewModel model)
        {
            ServicesRepo.Add(model);
            unitOfWork.Save();
           
            return RedirectToAction("ProviderGet", new { ProviderID =model.ProviderId});
        }

        public ResultViewModel AddApi (ServicesAddViewModel model)
        {
            ServicesRepo.Add(model);
            unitOfWork.Save();

            return new ResultViewModel()
            {
                Message = "Added Successfuuly",
                Success = true,
                Data = null
            };
        }

        public IActionResult ServicesAccepted(int id)
        {
            ServicesRepo.ServicesAccepted(id);
            unitOfWork.Save();
            return RedirectToAction("ProviderGet");

        }

        public IActionResult ServicesRejected(int id)
        {
            ServicesRepo.ServicesRejected(id);
            unitOfWork.Save();
            return RedirectToAction("ProviderGet");

        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var clamisidentity = (System.Security.Claims.ClaimsIdentity)this.User.Identity;
            var claim = clamisidentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            var userid = claim.Value;
            int provider_Id = UserRepository.Detailes(userid);
            ViewBag.Provider = provider.getproviderbyUserID(userid);
            ServicesAddViewModel? data = ServicesRepo.GetByID(id);
            ViewBag.Categories = GetCategoryesDropDownList();
            return View(data);
        }

        [HttpPost]
        public IActionResult Edit(ServicesAddViewModel model)
        {


            ServicesRepo.Update(model);
            unitOfWork.Save();
            return RedirectToAction("ProviderGet", new { ProviderID = model.ProviderId });


        }

        public IActionResult Remove(int id)
        {
            var clamisidentity = (System.Security.Claims.ClaimsIdentity)this.User.Identity;
            var claim = clamisidentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            var userid = claim.Value;
            int provider_Id = UserRepository.Detailes(userid);
            ViewBag.Provider = provider.getproviderbyUserID(userid);
            ServicesRepo.SoftDeleted(id);
            unitOfWork.Save();
            return RedirectToAction("ProviderGet");

        }

        private IEnumerable<SelectListItem> GetCategoryesDropDownList() =>
            (CatRepo.GetDropDownValues()).Select
               (i => new SelectListItem
               {
                   Text = i.Text,
                   Value = i.Value.ToString()
               });


        public IActionResult Details(int id)
        {
            var result = ServicesRepo.GetByID(id);
            return View(result);
        }

        public IActionResult ProviderDetails(int id)
        {
            var result = ServicesRepo.GetByID(id);
            return View(result);
        }

        public ResultViewModel GetServicesAPI(string Name = "", float Price = 0, string orderBy = "", bool IsAscending = false, int PageIndex = 10, int PageSize = 10)
        {
            var result = ServicesRepo.GetAPI(Name, Price, orderBy, IsAscending, PageIndex, PageSize);

            return new ResultViewModel()
            {
                Success = true,
                Message = "",
                Data = result

            };

        }

        public ResultViewModel ServicesDetailsAPI(int id)
        {
            var result = provider.GetServicesDetailsAPI(id);
            return new ResultViewModel
            {
                Success = true,
                Message = "",
                Data = result
            };
        }


    }
}
