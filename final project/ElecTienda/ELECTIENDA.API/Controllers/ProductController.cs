using ELECTIENDA.Repository;
using Microsoft.AspNetCore.Mvc;
using ELECTIENDA.ViewModel;

namespace ElECTIENDA.MVC
{
    public class ProductController : Controller
    {

        ProductRepository productRepo;
        UnitOfWork unitOfWork;

        public ProductController(ProductRepository _productRepo, UnitOfWork _unitOfWork)
        {
            productRepo = _productRepo;
            unitOfWork = _unitOfWork;
        }

        public IActionResult Get(string name = null, float price = 0, string color = null, string orderBy = null, bool isAscending = false, int pageIndex = 1, int pageSize = 20)
        {
            var data =
             productRepo.GetProducts(name, price, color, orderBy, isAscending, pageIndex, pageSize);
            return View(data);
        }

        public IActionResult AcceptProduct(int _id)
        {
            productRepo.productAccept(_id);

            return RedirectToAction("Get");
        }

        public IActionResult Search(string name = null, float price = 0, string color = null, string orderBy = null, bool isAscending = false, int pageIndex = 1, int pageSize = 20)
        {
            var data = productRepo.Search(name, price, color, orderBy, isAscending, pageIndex, pageSize);
            return View("Get", data);
        }

        public IActionResult ProviderSearch(string name = null, float price = 0, string color = null, string orderBy = null, bool isAscending = false, int pageIndex = 1, int pageSize = 20)
        {
            var data = productRepo.Search(name, price, color, orderBy, isAscending, pageIndex, pageSize);
            return View( data);
        }

        public IActionResult ProductAccepted(int id)
        {


            productRepo.ProductAccepted(id);
            unitOfWork.Save();
            return RedirectToAction("Search");

        }

        public IActionResult ProductRejected(int id)
        {
            productRepo.ProductRejected(id);
            unitOfWork.Save();
            return RedirectToAction("Search");

        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Add(ProductEditViewModel model)
        {
            if (ModelState.IsValid == true)
            {
                productRepo.Add(model);
                unitOfWork.Save();
                return RedirectToAction("Search");

            }
            else
            {
                ModelState.AddModelError("", "You Have 3 Times At Most To Try !!");
                return View();

            }


        }

        public IActionResult Remove(int id)
        {
            productRepo.Remove(id);
            unitOfWork.Save();
            return RedirectToAction("Search");

        }

        //public IActionResult Delete(int id)
        //{
        //    productRepo.Delete(id);
        //    unitOfWork.Save();
        //    return RedirectToAction("Search");

        //}

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var data = productRepo.GetById(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult Edit(ProductEditViewModel model)
        {


            productRepo.Update(model);
            unitOfWork.Save();
            return RedirectToAction("Search");


        }

    }
}
