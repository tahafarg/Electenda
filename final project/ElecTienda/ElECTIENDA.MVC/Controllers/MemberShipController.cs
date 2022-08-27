using Microsoft.AspNetCore.Mvc;
using ELECTIENDA.Repository;
using ELECTIENDA.ViewModel;

namespace ElECTIENDA.MVC.Controllers
{
    public class MemberShipController : Controller
    {
        private readonly MembershipRepository MembershipRepo;
        
        private readonly UnitOfWork unitOfWork;
        public MemberShipController(MembershipRepository _MemberShipRepo, UnitOfWork _UnitOfWork, CategoryRepository _CatRepo)
        {
            MembershipRepo = _MemberShipRepo;
            unitOfWork = _UnitOfWork;
            
        }
        public IActionResult AdminGet(string name = "",
             string orderby = "Id", bool isAscending = false,
             int pageIndex = 1, int pageSize = 20)
        {


            var result = MembershipRepo.Get(name, orderby, isAscending, pageIndex,
                             pageSize);
            return View(result);
        }

        [HttpGet]
        public IActionResult Add()
        {
           
            return View();
        }
        [HttpPost]
        public IActionResult Add(MemberShipEditViewModel model)
        {
            MembershipRepo.Add(model);
            unitOfWork.Save();

            return RedirectToAction("AdminGet");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            MemberShipEditViewModel? data = MembershipRepo.GetByID(id);
            
            return View(data);
        }

        [HttpPost]
        public IActionResult Edit(MemberShipEditViewModel model)
        {


            MembershipRepo.Update(model);
            unitOfWork.Save();
            return RedirectToAction("AdminGet");


        }

        public IActionResult Remove(int id)
        {
            MembershipRepo.SoftDeleted(id);
            unitOfWork.Save();
            return RedirectToAction("AdminGet");

        }

    }
}
