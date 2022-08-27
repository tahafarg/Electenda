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
        private readonly ProviderRepository providerRepo;

        public UserController(UserAdminRepository _userRep,ProviderRepository provider , UnitOfWork _unitOfWork , UserRepository userRepository)
        {
            userRep = _userRep;
            unitOfWork = _unitOfWork;
            UserRepository = userRepository;
            providerRepo = provider;
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
                   string Address = "", string orderBy = "ID",int? ProviderId =null
            , bool isAscending = false, int pageIndex = 1, int pageSize = 10)
        {
            var data = userRep.Search2(FirstName, LastName, Address,ProviderId
            , orderBy, isAscending, pageIndex, pageSize);
            return View(data);
        }
        [HttpGet]
        public IActionResult SignIn()
        {
            //return new ObjectResult(new
            //{
            //    Message = "Please Login",
            //    LoginUrl = "User/Login"
            //});
            return View("SignIn");

        }
        [HttpPost]
        public async Task<IActionResult> SignIn(UserLogInViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await providerRepo.SignIn(model);
                //string token
                //        = await UserRepository.SignIn(model);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Invalid User Name Or Password");
                }
                else
                {
                    if (User.HasClaim(c => c.Value == "Provider"))
                        return RedirectToAction("index", "Provider");
                    if (User.HasClaim(c => c.Value == "Admin"))
                        return RedirectToAction("Index", "Admin");
                }
            }
            //}
            //List<string> errors = new List<string>();
            //foreach (var i in ModelState.Values)
            //    foreach (var e in i.Errors)
            //        errors.Add(e.ErrorMessage);
            return View();

        }


        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> SignUp(UserEditViewModel model)
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
        [HttpPost]

        public async Task<ResultViewModel> SignUpapi([FromBody] UserEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result
                        = await UserRepository.SignUp(model);
                if (!result.Succeeded)
                {
                    //foreach (var err in result.Errors)
                    //  ModelState.AddModelError("", err.Description);
                    return new ResultViewModel()
                    {
                        Success = false,
                        Message = "try Again",
                        Data = result.Errors
                    };
                }
                else
                {
                    //return RedirectToAction("SignIn", "user");
                    return new ResultViewModel()
                    {
                        Success = true,
                        Message = "SignIn / UserApi",
                        Data = null
                    };
                }
            }

            List<string> error = new List<string>();
            foreach (var Val in ModelState.Values)
                foreach (var err in Val.Errors)
                    error.Add(err.ErrorMessage);
            return new ResultViewModel()
            {
                Success = false,
                Message = "try again",
                Data = error.ToString()
            };
        

            //    string token
            //            = await UserRepository.SignIn(model);
            //    if (string.IsNullOrEmpty(token))
            //    {
            //        ModelState.AddModelError("", "Invalid User Name Or Password");
            //    }
            //    else
            //    {

            //        return RedirectToAction("index", "Provider");
            //    }
            //}
            //List<string> errors = new List<string>();
            //foreach (var i in ModelState.Values)
            //    foreach (var e in i.Errors)
            //        errors.Add(e.ErrorMessage);
            //return View();

        }

        [HttpPost]

        public async Task<ResultViewModel> SignInapi([FromBody] UserLogInViewModel model)
        {
            if (ModelState.IsValid)
            {

                string token
                        = await UserRepository.SignIn(model);
                if (string.IsNullOrEmpty(token))
                {
                    ModelState.AddModelError("", "Invalid User Name Or Password");
                }
                else
                {
                    var userId = UserRepository.Getuserid(model.Email);
                    return new ResultViewModel
                    {
                        Success = true,
                        Data = new
                        {
                            token = token,
                            userId = userId
                        }
                    };
                }
            }
            List<string> errors = new List<string>();
            foreach (var i in ModelState.Values)
                foreach (var e in i.Errors)
                    errors.Add(e.ErrorMessage);
            return new ResultViewModel
            {
                Data = null,
                Success = true,

            };
        }

            [HttpGet]
        public new async Task<IActionResult> SignOut()
        {
            await UserRepository.SignOut();
            return RedirectToAction("SignIn", "User");
        }
        [HttpGet]
        public new async Task<ResultViewModel> SignOutapi()
        {
            await UserRepository.SignOut();
            //return RedirectToAction("SignIn", "User");
            return new ResultViewModel
            {
                Data = "",
                Success = true,
                Message = ""
            };
        }
        public IActionResult Delete(string ID)
        {
            var result = userRep.Remove(ID);
            unitOfWork.Save();
            return RedirectToAction("Search");
        }



    }

}
