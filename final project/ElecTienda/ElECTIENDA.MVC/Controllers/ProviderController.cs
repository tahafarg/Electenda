using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ELECTIENDA.Repository;
using ELECTIENDA.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ElECTIENDA.MVC
{

    public class ProviderController : Controller
    {

        ProviderRepository provRebo;
        MembershipRepository MemRepo;
        UnitOfWork unitOfWork;
        private readonly ProductRepository prodRep;
        private readonly CategoryRepository catRep;
        private readonly ShopRepository shopRep;
        private readonly UserRepository UserRepo;

        public ProviderController(ProviderRepository _provRebo, MembershipRepository _MemRepo, UnitOfWork _unitOfWork,
             ProductRepository _prodRep, CategoryRepository _catRep, BrandRepository _brandRep, ShopRepository _shopRep,UserRepository _UserRepo)
        {
            provRebo = _provRebo;
            MemRepo = _MemRepo;
            unitOfWork = _unitOfWork;
            UserRepo = _UserRepo;
            prodRep = _prodRep;
            catRep = _catRep;
            shopRep = _shopRep;
        }
        public IActionResult Get(int id = 0, string name = "", string phone = "", string orderBy = "ProviderID", bool isAscending = false, int pageIndex = 1, int pageSize = 20)
        {

            var data =
            provRebo.GetProviders(id, name, phone, orderBy, isAscending, pageIndex, pageSize);

            return View(data);
        }
        public IActionResult Search(int id = 0, string Name = null, string orderBy = null, bool isAscending = false, int pageIndex = 1, int pageSize = 20)
        {
            var data = provRebo.Search(id, Name, orderBy, isAscending, pageIndex, pageSize);
            return View("Get", data);
        }
        public IActionResult Remove(int id)
        {
            provRebo.Remove(id);
            unitOfWork.Save();
            return RedirectToAction("Search");

        }
        //public IActionResult Delete(int id)
        //{
        //    provRebo.Remove(id);
        //    unitOfWork.Save();
        //    return RedirectToAction("Search");

        //}


        [HttpGet]
        public IActionResult SignUp()
        {
            ViewBag.Memberships = GetMembershipsDropDownList();
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> SignUp(ProviderAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                string? bookUploadUrl = "/Content/Uploads/Provider/";
                string newFileName = Guid.NewGuid().ToString() + model.Image.FileName;
                model.ImageUrl = bookUploadUrl + newFileName;
                FileStream? fs = new FileStream(Path.Combine
                    (
                        Directory.GetCurrentDirectory(),
                        "Content",
                       "Uploads", "Provider", newFileName
                    ), FileMode.Create);
                model.Image.CopyTo(fs);
                fs.Position = 0;

                string? LicenceUploadUrl = "/Content/Uploads/ProviderLicence/";
                string newFileNameLicence = Guid.NewGuid().ToString() + model.LicenseImg.FileName;
                model.LicenseImageUrl = LicenceUploadUrl + newFileNameLicence;
                FileStream? Fs = new FileStream(Path.Combine
                    (
                        Directory.GetCurrentDirectory(),
                        "Content",
                       "Uploads", "Provider", newFileNameLicence
                    ), FileMode.Create);
                model.LicenseImg.CopyTo(Fs);
                fs.Position = 0;


                IdentityResult result
                        = await provRebo.SignUp(model);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                }
                else
                {


                    return RedirectToAction("SignIn", "User");
                }
            }

            //ViewBag.Memberships = GetMembershipsDropDownList();
            return View();
        }

        [Authorize (Roles = "Provider")]
       
        public IActionResult index()
        {

            //var acc = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var _user = User.HasClaim(c => c.Value == "Admin");
            //var _user2 = User.HasClaim(c => c.Value == "Provider");
            //var _user3 = User.HasClaim(c => c.Value == "User");
            var clamisidentity = (System.Security.Claims.ClaimsIdentity)this.User.Identity;
            var claim = clamisidentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            var UserId = claim.Value;
            ViewBag.Provider = provRebo.getproviderbyUserID(UserId);
            //ViewBag.uid = providerid;
            ViewBag.prodCount = prodRep.GetList().Count();
            ViewBag.catCount = catRep.GetList().Count();
            ViewBag.shopCount = shopRep.GetList().Count();
            
            return View("Provider");
         
        }
        private IEnumerable<SelectListItem> GetMembershipsDropDownList() =>
        (MemRepo.GetDropDownValues()).Select
    (i => new SelectListItem
    {
        Text = i.Text,
        Value = i.Value.ToString()
    });

    }
}
    


