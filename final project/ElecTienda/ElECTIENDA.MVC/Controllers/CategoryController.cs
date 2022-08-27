using Microsoft.AspNetCore.Mvc;
using ELECTIENDA.Repository;

using ELECTIENDA.ViewModel;
using Microsoft.AspNetCore.Authorization;

namespace ElECTIENDA.MVC
{
    public class CategoryController : Controller
    {
        private readonly CategoryRepository CatRepo;
        private readonly UnitOfWork unitOfWork;
        private readonly UserRepository UserRepo;
        private readonly ProviderRepository ProviderRepo;

        public CategoryController(ProviderRepository provider,CategoryRepository _CateRepo,UserRepository user, UnitOfWork _UnitOfWork)
        {
            CatRepo = _CateRepo;
            unitOfWork = _UnitOfWork;
            UserRepo = user;
            ProviderRepo = provider;
        }
        public IActionResult Get(string? name = "",
            string? orderBy = "ID", bool isAscending = false, int pageIndex = 1,
                    int pageSize = 10)
        {
            PaginingViewModel<List<CategoryViewModel>> result
                = CatRepo.Get(name, orderBy, isAscending, pageIndex,
                             pageSize);
            return View(result.Data);

            var data = CatRepo.Search2(name, orderBy, isAscending, pageIndex, pageSize);

            return View(data);


        }

        public ResultViewModel Getapi(string? name = "",
            string? orderBy = "ID", bool isAscending = false, int pageIndex = 1,
                    int pageSize = 10)
        {
            var result = CatRepo.Get(name, orderBy, isAscending, pageIndex,
                             pageSize); 
            return new ResultViewModel
            {
                Data = result.Data
            };
        }
        public ResultViewModel getCategoryforList()
        {
            var res = CatRepo.GetDropDownValues();
            return new ResultViewModel
            {
                Data = res,
            };
        }
        public IActionResult AdminSearch(string? name = "",
           string? orderBy = "ID", bool isAscending = false, int pageIndex = 1,
                   int pageSize = 10)
        {
           

            var data = CatRepo.Search2(name, orderBy, isAscending, pageIndex, pageSize);

            return View(data);


        }

        public IActionResult ProviderSearch(string? name = "",
           string? orderBy = "ID", bool isAscending = false, int pageIndex = 1,
                   int pageSize = 10)
        {
            var clamisidentity = (System.Security.Claims.ClaimsIdentity)this.User.Identity;
            var claim = clamisidentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            var UserId = claim.Value;
            ViewBag.Provider = ProviderRepo.getproviderbyUserID(UserId);
            var data = CatRepo.Search2(name, orderBy, isAscending, pageIndex, pageSize);

            return View(data);
        }


        public IActionResult GetByResult(int Id)
        {
            var result = CatRepo.GetByID(Id);
            return View(result);
        }



        //[Authorize]
        [HttpGet]
        public IActionResult Add()
        {

            var clamisidentity = (System.Security.Claims.ClaimsIdentity)this.User.Identity;
            var claim = clamisidentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            var userid = claim.Value;
            int provider_Id = UserRepo.Detailes(userid);
            return View();
        }

        //[Authorize]
        [HttpPost]
        public IActionResult Add(CategoryEditViewModel model)
        {
            CatRepo.Add(model);
            unitOfWork.Save();
           
           
            return RedirectToAction("AdminSearch");
        }



        //public IActionResult Add(CategoryEditViewModel model)

        //{
        //    if (ModelState.IsValid == true)
        //    {
        //        string? categoryUploadUrl = "/Content/Uploads/Category/";
        //        //model.Imgsrc = new List<string>();
        //        string newFileName = Guid.NewGuid().ToString() + model.Imgsrc;
        //        //model.Imgsrc=(categoryUploadUrl + newFileName);

        //            FileStream fs = new FileStream(Path.Combine
        //                (
        //                    Directory.GetCurrentDirectory(),
        //                    "Content",
        //                   "Uploads", "Category", newFileName
        //                ), FileMode.Create);

        //            model.Imgsrc.;
        //            fs.Position = 0;
        //        }

        //        CatRepo.Add(model);
        //        UnitOfWork.Save();

        //        return View();
        //    }
        //    else
        //    {

        //        ModelState.AddModelError("", "You Have 3 Times At Most To Try !!");
        //        ViewBag.Publishers = GetpublishersDropDownList();
        //        return View();
        //    }

        public IActionResult CategoryAccepted(int id)
        {
            CatRepo.CategoryAccepted(id);
            unitOfWork.Save();
            return RedirectToAction("AdminSearch");

        }

        public IActionResult CategoryRejected(int id)
        {
            CatRepo.CategoryRejected(id);
            unitOfWork.Save();
            return RedirectToAction("AdminSearch");

        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var data = CatRepo.GetByID(id);
            //ViewBag.Categories = GetCategoryesDropDownList();
            return View(data);
        }

        [HttpPost]
        public IActionResult Edit(CategoryEditViewModel model)
        {


            CatRepo.UpdateCat(model);
            unitOfWork.Save();
            return RedirectToAction("AdminSearch");


        }

        public IActionResult Remove(int id)
        {
            CatRepo.SoftDeleted(id);
            unitOfWork.Save();
            return RedirectToAction("AdminSearch");

        }

    }
}

