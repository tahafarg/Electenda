
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

        public IActionResult get(int id = 0, string name = "", string orderby = "id"
            , bool isascending = false, int pageindex = 1, int pagesize = 5)
        {
            PaginingViewModel<List<BrandViewModel>> result
            = BrandRepo.Get(id, name, orderby, isascending, pageindex, pagesize);
            return View(result.Data);
        }
    }
}
