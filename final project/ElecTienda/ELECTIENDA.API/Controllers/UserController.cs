using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ELECTIENDA.Repository;
using ELECTIENDA.ViewModel;
using Microsoft.AspNetCore.Identity;

namespace ElECTIENDA.MVC
{
    public class UserController : Controller
    {
        private readonly UserAdminRepository userRep;
        private readonly UnitOfWork unitOfWork;
        private readonly UserRepository UserRepository;

        public UserController(UserAdminRepository _userRep , UnitOfWork _unitOfWork , UserRepository userRepository)
        {
            userRep = _userRep;
            unitOfWork = _unitOfWork;
            UserRepository = userRepository;
        }


        public IActionResult Get(string FirstName = "",string LastName = "",
                   string Address = "", string phone = "",string orderBy = "ID"
            , bool isAscending = false, int pageIndex = 1,int pageSize = 20)
        {
            var data =
            userRep.Get(FirstName , LastName , Address , phone 
            , orderBy , isAscending , pageIndex, pageSize);
            return View(data);
        }

        //public IActionResult Search(int pageIndex = 1, int pageSize = 10)
        //{
        //    var data = userRep.Search(pageIndex, pageSize);
        //    return View(data);
        //}

        public IActionResult Search(string FirstName = "", string LastName = "",
                   string Address = "", string orderBy = "ID"
            , bool isAscending = false, int pageIndex = 1, int pageSize = 10)
        {
            var data = userRep.Search2(FirstName, LastName, Address
            , orderBy, isAscending, pageIndex, pageSize);
            return View(data);
        }

        [HttpGet]
        public IActionResult SignUp ()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> SignUp (UserEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityResult identity = await UserRepository.SignUp(model);
                if (!identity.Succeeded)
                {
                    foreach (var error in identity.Errors)
                        ModelState.AddModelError("", error.Description);
                }
                else
                {
                    string? imgUploadUrl = "/Content/Uploads/User/";
                   model.ImageUrl = new string(imgUploadUrl);
                   
                        string newFileName = Guid.NewGuid().ToString() + model.Image;

                    model.ImageUrl = imgUploadUrl + newFileName;
                   

                    return RedirectToAction("SignIn", "User");
                }
                   
            }
            return View();
        }

        [HttpGet]
        public IActionResult SignIn ()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(UserLogInViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result
                        = await UserRepository.SignIn(model);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Invalid User Name Or Password");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        [HttpGet]
        public new async Task<IActionResult> SignOut()
        {
            await UserRepository.SignOut();
            return RedirectToAction("SignIn", "User");
        }


        public IActionResult Delete(string ID)
        {
            var result = userRep.Remove(ID);
            unitOfWork.Save();
            return RedirectToAction("Search");
        }


        //public IActionResult Show(string ID)
        //{
        //   var result = userRep.GetByID(ID);
        //    return View(result);
        //}


    }

}
