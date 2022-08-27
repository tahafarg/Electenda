using Microsoft.AspNetCore.Mvc;
using ELECTIENDA.Repository;

using ELECTIENDA.ViewModel;
using Microsoft.AspNetCore.Authorization;

namespace ElECTIENDA.MVC
{
    public class CategoryController : Controller
    {
        private readonly CategoryRepository CatRepo;
        private readonly UnitOfWork unitOfWork;
        public CategoryController(CategoryRepository _CateRepo, UnitOfWork _UnitOfWork)
        {
            CatRepo = _CateRepo;
            unitOfWork = _UnitOfWork;
        }
        public IActionResult Get(string? name = "",
            string? orderBy = "ID", bool isAscending = false, int pageIndex = 1,
                    int pageSize = 20)
        {
            PaginingViewModel<List<CategoryViewModel>> result
                = CatRepo.Get(name, orderBy, isAscending, pageIndex,
                             pageSize);
            return View(result.Data);
        }
        public IActionResult GetByResult(int Id)
        {
            var result = CatRepo.GetByID(Id);
            return View(result);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add(CategoryEditViewModel model)
        {
            CatRepo.Add(model);
            unitOfWork.Save();
            return RedirectToAction("Get");
        }
    }
}

