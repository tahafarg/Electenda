using Microsoft.AspNetCore.Mvc;
using ELECTIENDA.Repository;
using ELECTIENDA.ViewModel;

namespace ElECTIENDA.MVC.Controllers
{
    public class FavoriteController : Controller
    {
        private readonly FavoriteRepository Favorite_Repo;
        private readonly UnitOfWork unitOfWork;

        public FavoriteController(UnitOfWork _UnitOfWork, FavoriteRepository _Favorite_Repo)
        {
            Favorite_Repo = _Favorite_Repo;
            unitOfWork = _UnitOfWork;

        }
        public ViewResult Get(int ID = 0, int ServicesId = 0, string ServicesName = "",
            string UserId = "", int ProductId = 0, string ProductName = "", int Quantity = 0,
            string orderby = "ID", bool IsDeleted = false, bool isAscending = false, int pageIndex = 1,
            int pageSize = 20)

        {
            PaginingViewModel<List<FavoriteViewModel>>
                result
             = Favorite_Repo.Get(ID, ServicesId, ServicesName, UserId, ProductId
             , ProductName, Quantity, orderby, IsDeleted, isAscending, pageIndex, pageSize);
            return View(result.Data);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(FavoriteEditViewModel model)
        {
            Favorite_Repo.Add(model);
            unitOfWork.Save();
            return RedirectToAction("Get");
        }
        [HttpGet]
        public IActionResult Update(int Id)
        {
            var Results = Favorite_Repo.GetbyID(Id);

            return View(Results);

        }
        [HttpPost]
        public IActionResult Update(FavoriteEditViewModel model, int ID = 0)
        {
            Favorite_Repo.Update(model);
            unitOfWork.Save();
            return RedirectToAction("Get");

        }
        [HttpGet]
        public IActionResult Remove(int id)
        {
            Favorite_Repo.isDeleted(id);
            unitOfWork.Save();
            return RedirectToAction("Get");

        }

        public ResultViewModel Getfororder(string userid)
        {
            var result = Favorite_Repo.getForOrder(userid);
            return new ResultViewModel
            {
                Data = result,
                Message = "",
                Success = true,

            };
        }


        [HttpPost]
        public ResultViewModel Addapi([FromBody] FavoriteEditViewModel model)
        {
            if (Favorite_Repo.GetList().Where(c => c.ProductID == model.ProductID && c.UserID == model.UserID).Count() > 0)
            {
                return new ResultViewModel()
                {
                    Message = "already in cart",
                    Success = true,
                    Data = null
                };
            }
            else
            {

                Favorite_Repo.Add(model);
                unitOfWork.Save();

                return new ResultViewModel()
                {
                    Message = "Added Successfuuly",
                    Success = true,
                    Data = null
                };
            }
        }

        public ResultViewModel Deleteapi(int id)
        {
            var result = Favorite_Repo.GetbyID(id);
            Favorite_Repo.Remove(result);
            unitOfWork.Save();
            return new ResultViewModel()
            {
                Message = "Deleted Successfuuly",
                Success = true,
                Data = null
            };

        }


    }
}
