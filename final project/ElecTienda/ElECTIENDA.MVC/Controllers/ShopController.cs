using Microsoft.AspNetCore.Mvc;
using ELECTIENDA.Repository;
using ELECTIENDA.ViewModel;

namespace ElECTIENDA.MVC
{
    public class ShopController : Controller
    {
       private readonly ShopRepository SH_Repo;
        private readonly UnitOfWork UnitOfWork;
        private readonly ProviderRepository provRebo;
        private readonly UserRepository UserRepo;

        public ShopController(ShopRepository _SH_Repo,UserRepository user,ProviderRepository provider, UnitOfWork _UnitOfWork)
        {
            SH_Repo = _SH_Repo;
            UnitOfWork = _UnitOfWork;
            provRebo = provider;
            UserRepo = user;
        }
        public IActionResult Get(int ID = 0, int ProviderId = 0, string Name = ""
                              , string Address = "", string OrderBy = "ID"
                                , bool IsAscending = false, int pageIndex = 1, int pageSize = 5)
      
        {
            PaginingViewModel<List<ShopViewModel>> data =
                  SH_Repo.Get(ID,ProviderId,Name,Address,OrderBy,IsAscending,pageIndex,pageSize);
            return View(data.Data);
        }

        public IActionResult AdminSearch(int ID = 0, int ProviderId = 0, string Name = ""
                             , string Address = "", string OrderBy = "ID"
                               , bool IsAscending = false, int pageIndex = 1, int pageSize = 5)

        {
            var result =
                  SH_Repo.Search(ID, ProviderId, Name, Address, OrderBy, IsAscending, pageIndex, pageSize);
            return View(result);
        }

        public IActionResult ProviderSearch(int ID = 0, int ProviderId = 0, string Name = ""
                             , string Address = "", string OrderBy = "ID"
                               , bool IsAscending = false, int pageIndex = 1, int pageSize = 5)

        {

            var clamisidentity = (System.Security.Claims.ClaimsIdentity)this.User.Identity;
            var claim = clamisidentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            var UserId = claim.Value;
            ViewBag.Provider = provRebo.getproviderbyUserID(UserId);
            var result =
                  SH_Repo.Search(ID, ProviderId, Name, Address, OrderBy, IsAscending, pageIndex, pageSize);
            return View(result);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var clamisidentity = (System.Security.Claims.ClaimsIdentity)this.User.Identity;
            var claim = clamisidentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            var userid = claim.Value;
            int provider_Id = UserRepo.Detailes(userid);
            ViewBag.Provider = provRebo.getproviderbyUserID(userid);
            return View(new ShopEditViewModel { ProviderId=provider_Id});
        }
        [HttpPost]
        public IActionResult Add(ShopEditViewModel model)
        {
            string? ShopUploadUrl = "/Content/Uploads/Shop/";
            string newFileName = Guid.NewGuid().ToString() + model.ImgSrc.FileName;
            model.ImageUrl = ShopUploadUrl + newFileName;
            FileStream? fs = new FileStream(Path.Combine
                (
                    Directory.GetCurrentDirectory(),
                    "Content",
                   "Uploads", "Shop", newFileName
                ), FileMode.Create);
            model.ImgSrc.CopyTo(fs);
            fs.Position = 0;

            //var clamisIdentity = (System.Security.Claims.ClaimsIdentity)this.User.Identity;
            //var claim = clamisIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            //var ProviderId = claim.Value;
            // ViewBag.ProviderId = ProviderId;
            SH_Repo.add(model);
            UnitOfWork.Save();
            return RedirectToAction("ProviderSearch", new { ProviderID = model.ProviderId });
        }


        public IActionResult ShopAccepted(int id)
        {
            SH_Repo.ShopAccepted(id);
            UnitOfWork.Save();
            return RedirectToAction("AdminSearch");

        }

        public IActionResult ShopRejected(int id)
        {
            SH_Repo.ShopRejected(id);
            UnitOfWork.Save();
            return RedirectToAction("AdminSearch");

        }


        [HttpGet]
        public IActionResult Edit(int id)
        {

            var clamisidentity = (System.Security.Claims.ClaimsIdentity)this.User.Identity;
            var claim = clamisidentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            var userid = claim.Value;
            int provider_Id = UserRepo.Detailes(userid);
            ViewBag.Provider = provRebo.getproviderbyUserID(userid);
            ShopEditViewModel? data = SH_Repo.GetByID(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult Edit(ShopEditViewModel model)
        {

            string? ShopUploadUrl = "/Content/Uploads/Shop/";
            string newFileName = Guid.NewGuid().ToString() + model.ImgSrc.FileName;
            model.ImageUrl = ShopUploadUrl + newFileName;
            FileStream? fs = new FileStream(Path.Combine
                (
                    Directory.GetCurrentDirectory(),
                    "Content",
                   "Uploads", "Shop", newFileName
                ), FileMode.Create);
            model.ImgSrc.CopyTo(fs);
            fs.Position = 0;

            SH_Repo.Update(model);
            UnitOfWork.Save();
            return RedirectToAction("ProviderSearch", new { ProviderID = model.ProviderId });


        }

        public IActionResult Remove(int id)
        {

            var clamisidentity = (System.Security.Claims.ClaimsIdentity)this.User.Identity;
            var claim = clamisidentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            var userid = claim.Value;
            int provider_Id = UserRepo.Detailes(userid);
            ViewBag.Provider = provRebo.getproviderbyUserID(userid);
            SH_Repo.SoftDeleted(id);
            UnitOfWork.Save();
            return RedirectToAction("ProviderSearch");

        }



    }
}

