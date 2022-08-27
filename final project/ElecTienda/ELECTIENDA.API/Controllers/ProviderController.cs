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

namespace ElECTIENDA.MVC
{
    public class ProviderController : Controller
    {
        private readonly ProviderRepository provRepo;
        private readonly UnitOfWork unitOfWork;

        public ProviderController(ProviderRepository _provRepo, UnitOfWork _unitOfWork)
        {
            provRepo = _provRepo;
            unitOfWork = _unitOfWork;
        }


        public ViewResult Get(int id = 0, string name = "", string phone = "", string orderBy = "ProviderID", bool isAscending = false, int pageIndex = 1, int pageSize = 20)
        {

            var data =
            provRepo.GetProviders(id, name, phone, orderBy, isAscending, pageIndex, pageSize);

            return View(data);
        }

        public IActionResult Search (int Id =0,string? Name=null ,string? orderBy= null, bool isAscending =false,int pageIndex=1,int pageSize=20 )
        {
            var data = provRepo.Search(Id, Name, orderBy, isAscending, pageIndex, pageSize);
            return View("Get", data);
        }

        public IActionResult Remove(int id)
        {
        provRepo.Remove(id);
        unitOfWork.Save();
        return RedirectToAction("Search");
        }



        //public IActionResult Delete(int id)
        //{
        //    provRepo.Delete(id);
        //    unitOfWork.Save();
        //    return RedirectToAction("Search");

        //}


        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(ProviderAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result
                        = await provRepo.SignUp(model);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                    // return View(result);
                }
                else
                {
                    return RedirectToAction("SignIn", "User");
                }
            }
            return View();


        }
    }
}

