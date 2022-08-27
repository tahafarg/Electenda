
using Microsoft.AspNetCore.Mvc;

using ELECTIENDA.Repository;
using ELECTIENDA.ViewModel;

namespace electienda.mvc
{
    public class brandcontroller : Controller
    {
        
        private readonly BrandRepository BrandRepo;
        private readonly UnitOfWork unitOfWork;
        public brandcontroller(BrandRepository _BrandRepo, UnitOfWork _UnitOfWork)
        {
            BrandRepo = _BrandRepo;
            unitOfWork = _UnitOfWork;
        }

        public IActionResult Get(int id = 0, string name = "", string orderby = "id"
            , bool isascending = false, int pageindex = 1, int pagesize = 5)
        {
            //PaginingViewModel<List<BrandViewModel>> result
            //= BrandRepo.Get(id, name, orderby, isascending, pageindex, pagesize);
            //return View(result.Data);

            var data = BrandRepo.Search2( id ,  name ,  orderby 
            ,  isascending ,  pageindex = 1,  pagesize = 10);
            return View(data);


        }






        [HttpGet]
        public IActionResult Edit(int id)
        {
            var data = BrandRepo.GetbyID(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult Edit(BrandEditViewModel model)
        {


            BrandRepo.UpdateBrand(model);
            unitOfWork.Save();
            return RedirectToAction("Get");


        }

        public IActionResult Remove(int id)
        {
            BrandRepo.SoftDeleted(id);
            unitOfWork.Save();
            return RedirectToAction("Get");

        }

        public ResultViewModel Getapi(int id = 0, string name = "", string orderby = "id"
            , bool isascending = false, int pageindex = 1, int pagesize = 5)
        {
           var result= BrandRepo.Get(id, name, orderby
            , isascending, pageindex, pagesize);

            return new ResultViewModel
            {
                Data = result.Data,
                Success = true ,
                Message = ""
            };

        }

    }
}
