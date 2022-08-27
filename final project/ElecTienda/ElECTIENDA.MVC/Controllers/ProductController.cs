using ELECTIENDA.Repository;
using Microsoft.AspNetCore.Mvc;
using ELECTIENDA.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace ElECTIENDA.MVC
{
    public class ProductController : Controller
    {

        ProductRepository productRepo;
        UnitOfWork unitOfWork;
        CategoryRepository CateRepo;
        UserRepository UserRepository;
        ProviderRepository provider;
        public ProductController(ProviderRepository repository,UserRepository user,ProductRepository _productRepo, CategoryRepository _CateRepo, UnitOfWork _unitOfWork)
        {
            productRepo = _productRepo;
            CateRepo = _CateRepo;
            unitOfWork = _unitOfWork;
            UserRepository = user;
            provider = repository;
        }


        //object clamisidentity = (System.Security.Claims.ClaimsIdentity)this.User.Identity;
        //var claim = clamisidentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
        //var userid = claim.Value;
        //int provider_Id = UserRepository.Detailes(userid);
        
        //public IActionResult Get(string name = null, float price = 0, string color = null, string orderBy = null, bool isAscending = false, int pageIndex = 1, int pageSize = 20)
        //{
        //    var data =
        //     productRepo.GetProducts(name, price, color, orderBy, isAscending, pageIndex, pageSize);
        //    return View(data);
        //}

    public ResultViewModel Get(int id, int ProviderId = 0,string providerName =null,string categoryName = null, float price = 0, int catId =0,string name = null, string color = null, string orderBy = null, bool isAscending = false, int pageIndex = 1, int pageSize = 10)
        {
            var result =
             productRepo.GetProducts(id , ProviderId, providerName, categoryName, price, catId,name,  color, orderBy, isAscending, pageIndex, pageSize);
            return new ResultViewModel
            {
                Message = "",
                Success = true,
                Data = result
            };
    }

       

        public IActionResult AcceptProduct(int _id)
        {
            productRepo.productAccept(_id);

            return RedirectToAction("Get");
        }
        [HttpGet]
        [Authorize]
        public IActionResult Search(int id, int ProviderId = 0, int catId = 0, string name = null, float price = 0, string color = null, string orderBy = null, bool isAscending = false, int pageIndex = 1, int pageSize = 20)
        {
            var data = productRepo.Search( id,ProviderId,  catId , name, price, color, orderBy, isAscending, pageIndex, pageSize);
            return new ObjectResult(new { message = "hello" });
      
        }

        public IActionResult ProviderSearch(int id, int ProviderId = 0, int catId = 0, string name = null, float price = 0, string color = null, string orderBy = null, bool isAscending = false, int pageIndex = 1, int pageSize = 20)
        {
            var clamisidentity = (System.Security.Claims.ClaimsIdentity)this.User.Identity;
            var claim = clamisidentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            var userid = claim.Value;
            int provider_Id = UserRepository.Detailes(userid);

           ViewBag.Provider = provider.getproviderbyUserID(userid);

            var data = productRepo.Search( id,  ProviderId ,  catId , name, price, color, orderBy, isAscending, pageIndex, pageSize);
            return View( data);
        }


        public IActionResult AdminSearch(int id, int ProviderId = 0, int catId = 0, string name = null, float price = 0, string color = null, string orderBy = null, bool isAscending = false, int pageIndex = 1, int pageSize = 20)
        {
            var data = productRepo.Search( id, ProviderId, catId , name, price, color, orderBy, isAscending, pageIndex, pageSize);
            return View(data);
        }
        public IActionResult ProductAccepted(int id)
        {
            productRepo.ProductAccepted(id);
            unitOfWork.Save();
            return RedirectToAction("AdminSearch");

        }

        public IActionResult ProductRejected(int id)
        {
            productRepo.ProductRejected(id);
            unitOfWork.Save();
            return RedirectToAction("AdminSearch");

        }

        [HttpGet]
        public IActionResult Add()
        {
            var clamisidentity = (System.Security.Claims.ClaimsIdentity)this.User.Identity;
            var claim = clamisidentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            var userid = claim.Value;
            int provider_Id = UserRepository.Detailes(userid);

            ViewBag.Provider = provider.getproviderbyUserID(userid);
  
            ViewBag.Categories = GetCategoriesDropDownList();
            return View(new ProductEditViewModel { ProviderID = provider_Id});
        }


        [HttpPost]
        public IActionResult Add(ProductEditViewModel model)
        {
            if (ModelState.IsValid == true)
            {
                string? bookUploadUrl = "/Content/Uploads/Product/";
                model.ImagesUrls = new List<string>();
                foreach (IFormFile file in model?.Images)
                {
                    string newFileName = Guid.NewGuid().ToString() + file.FileName;
                    model.ImagesUrls.Add(bookUploadUrl + newFileName);

                    FileStream fs = new FileStream(Path.Combine
                        (
                            Directory.GetCurrentDirectory(),
                            "Content",
                           "Uploads", "Product", newFileName
                        ), FileMode.Create);

                    file.CopyTo(fs);
                    fs.Position = 0;
                }
                productRepo.Add(model);
                unitOfWork.Save();
                return RedirectToAction("ProviderSearch", new {ProviderID = model.ProviderID });

            }
            else
            {
                ModelState.AddModelError("", "You Have 3 Times At Most To Try !!");
                ViewBag.Categories = GetCategoriesDropDownList();
                return View();

            }


        }

        public IActionResult Remove(int id)
        {

            var clamisidentity = (System.Security.Claims.ClaimsIdentity)this.User.Identity;
            var claim = clamisidentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            var userid = claim.Value;
            int provider_Id = UserRepository.Detailes(userid);
            ViewBag.Provider = provider.getproviderbyUserID(userid);
            productRepo.Remove(id);
            unitOfWork.Save();
            return RedirectToAction("AdminSearch");

        }

        public IActionResult ProviderRemove(int id)
        {

            var clamisidentity = (System.Security.Claims.ClaimsIdentity)this.User.Identity;
            var claim = clamisidentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            var userid = claim.Value;
            int provider_Id = UserRepository.Detailes(userid);
            ViewBag.Provider = provider.getproviderbyUserID(userid);
            productRepo.Remove(id);
            unitOfWork.Save();
            return RedirectToAction("ProviderSearch", new {ProviderID = provider_Id});

    }
    //public IActionResult Delete(int id)
    //{
    //    productRepo.Delete(id);
    //    unitOfWork.Save();
    //    return RedirectToAction("Search");

    //}

    [HttpGet]
        public IActionResult Edit(int id)
        {

            var clamisidentity = (System.Security.Claims.ClaimsIdentity)this.User.Identity;
            var claim = clamisidentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            var userid = claim.Value;
            int provider_Id = UserRepository.Detailes(userid);
            ViewBag.Provider = provider.getproviderbyUserID(userid);
            ViewBag.Categories = GetCategoriesDropDownList();

            var data = productRepo.GetById(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult Edit(ProductEditViewModel model)
        {

            // ViewBag.Provider = provider.getproviderbyUserID(userid);
            string? bookUploadUrl = "/Content/Uploads/Product/";
            model.ImagesUrls = new List<string>();
            foreach (IFormFile file in model?.Images)
            {
                string newFileName = Guid.NewGuid().ToString() + file.FileName;
                model.ImagesUrls.Add(bookUploadUrl + newFileName);

                FileStream fs = new FileStream(Path.Combine
                    (
                        Directory.GetCurrentDirectory(),
                        "Content",
                       "Uploads", "Product", newFileName
                    ), FileMode.Create);

                file.CopyTo(fs);
                fs.Position = 0;
            }
            productRepo.Update(model);
            unitOfWork.Save();
            return RedirectToAction("ProviderSearch", new { ProviderID = model.ProviderID});

        }

        private IEnumerable<SelectListItem> GetCategoriesDropDownList() =>
         (CateRepo.GetDropDownValues()).Select
            (i => new SelectListItem
            {
                Text = i.Text,
                Value = i.Value.ToString()
            });



        [HttpGet]
        public ResultViewModel getbyid(int id)
        {
            var result =
             productRepo.GetByIdapi(id);
            return new ResultViewModel
            {
                Message = "",
                Success = true,
                Data = result
            };
        }

    }
}
