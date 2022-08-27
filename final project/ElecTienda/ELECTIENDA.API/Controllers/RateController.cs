using Microsoft.AspNetCore.Mvc;
using ELECTIENDA.Repository;
using FirstLyer.Model;

using ELECTIENDA.ViewModel;

namespace ELECTIENDA.MVC
{
    public class RateController : Controller
    {
        private readonly RateRepository RateRepo;
        private readonly UnitOfWork UnitOfWork;
        public RateController(RateRepository _RateRepo , UnitOfWork _UnitOfWork)
        {
            RateRepo = _RateRepo;
            UnitOfWork = _UnitOfWork;
        }
        public IActionResult Get(decimal? rate = null, string ProductName = "",
                    string orderBy = "ID", bool isAscending = false, int pageIndex = 1,
                    int pageSize = 20)
        {
            PaginingViewModel<List<RateViewModel>> result
                = RateRepo.Get(rate, ProductName, orderBy, isAscending, pageIndex,
                             pageSize);
            return View(result.Data);
        }
        public IActionResult GetByResult(int Id) { 
        var result = RateRepo.GetByID(Id);
            return View(result);
        }

    

    }
}
