using Microsoft.AspNetCore.Mvc;
using ELECTIENDA.Repository;
using ELECTIENDA.ViewModel;
using Microsoft.AspNetCore.Authorization;

namespace ElECTIENDA.MVC
{
    public class AdminController : Controller
    {
        private readonly UserRepository userRep;
        private readonly ProviderRepository provRep;
        private readonly ProductRepository prodRep;
        private readonly CategoryRepository catRep;
        private readonly BrandRepository brandRep;
        private readonly ShopRepository shopRep;
        public AdminController(UserRepository _userRep, ProviderRepository _provRep , ProductRepository _prodRep ,
                          CategoryRepository _catRep , BrandRepository _brandRep , ShopRepository _shopRep )
        {
            userRep = _userRep;
            provRep = _provRep;
            prodRep = _prodRep;
            catRep = _catRep;
            brandRep = _brandRep;
            shopRep = _shopRep; 
        }
        [Authorize (Roles ="Admin")]
        public IActionResult Index()
        {
            ViewBag.userCount = userRep.GetList().Count();
            ViewBag.provCount = provRep.GetList().Count();
            ViewBag.prodCount = prodRep.GetList().Count();
            ViewBag.catCount = catRep.GetList().Count();
            ViewBag.brandCount = brandRep.GetList().Count();
            ViewBag.shopCount = shopRep.GetList().Count();
            return View("Admin");
        }
    }
}
