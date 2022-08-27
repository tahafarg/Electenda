using Microsoft.AspNetCore.Mvc;
using ELECTIENDA.Repository;
using ELECTIENDA.ViewModel;

namespace ElECTIENDA.MVC
{
    public class ShopController : Controller
    {
       private readonly ShopRepository SH_Repo;
        private readonly UnitOfWork UnitOfWork;
        public ShopController(ShopRepository _SH_Repo, UnitOfWork _UnitOfWork)
        {
            SH_Repo = _SH_Repo;
            UnitOfWork = _UnitOfWork;
        }
        public IActionResult Get(int ID = 0, int ProviderId = 0, string Name = ""
                              , string Address = "", string ImgSrc = "", string OrderBy = "ID"
                                , bool IsAscending = false, int pageIndex = 1, int pageSize = 5)
      
        {
            PaginingViewModel<List<ShopViewModel>> data =
                  SH_Repo.Get(ID,ProviderId,Name,Address,ImgSrc,OrderBy,IsAscending,pageIndex,pageSize);
            return View(data.Data);
        }
       

    }
}

