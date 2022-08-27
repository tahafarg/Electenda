using Microsoft.AspNetCore.Mvc;
using FirstLyer.Model;
using ELECTIENDA.Repository;
using ELECTIENDA.ViewModel;
using System.Security.Claims;

namespace ElECTIENDA.MVC.Controllers
{
    public class CartController : Controller
    {
        //private readonly ServicesRepository ServicesRepo;
        private readonly CartRepository CartRepo;
        private readonly UnitOfWork unitOfWork;
        private readonly OrderRepository OrderRepo;
        public CartController(UnitOfWork _UnitOfWork, CartRepository _CartRepo, OrderRepository _OrderRepo)
        {
            OrderRepo = _OrderRepo;
            unitOfWork = _UnitOfWork;
            CartRepo = _CartRepo;
        }
        public ResultViewModel get(int Id = 0, int? productId = 0, string UserId = "",
          string orderby = "id", bool isascending = false,
          int pageindex = 1, int pagesize = 10)
        {
            UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            PaginingViewModel<List<CartViewModel>> result
             = CartRepo.get(Id, productId, UserId, orderby, isascending, pageindex, pagesize);
            return new ResultViewModel()
            {
                Message = "Added Successfuuly",
                Success = true,
                Data = result.Data
            };

        }



        public ResultViewModel Search(int Id = 0, string UserId = "",
          string orderby = "id", bool isascending = false,
          int pageindex = 1, int pagesize = 0)
        {
            UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result
             = CartRepo.Search2(Id, UserId, orderby, isascending, pageindex, pagesize);
            return new ResultViewModel()
            {
                Message = "Added Successfuuly",
                Success = true,
                Data = result
            };
        }

        //public IActionResult Add()
        //{

        //    return View();
        //}

        [HttpPost]
        public ResultViewModel Add([FromBody] CartAddViewModel model)
        {
            if (CartRepo.GetList().Where(c => c.ProductID == model.ProductID && c.UserID == model.UserID).Count() > 0)
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

                CartRepo.Add(model);
                unitOfWork.Save();

                return new ResultViewModel()
                {
                    Message = "Added Successfuuly",
                    Success = true,
                    Data = null
                };
            }
        }
        [HttpGet]
        public IActionResult Update(int Id)
        {
            var Results = CartRepo.GetById(Id);

            return View(Results);
        }

        public ResultViewModel Delete(int id)
        {
            var result = CartRepo.GetById(id);
            CartRepo.Remove(result);
            unitOfWork.Save();
            return new ResultViewModel()
            {
                Message = "Deleted Successfuuly",
                Success = true,
                Data = null
            };

        }

        [HttpPut]
        public ResultViewModel Editapi([FromBody] CartViewModel model)
        {

            var cart = CartRepo.GetById(model.ID);
            cart.Quantity = model.Quantity;
            CartRepo.Update(cart);
            unitOfWork.Save();
            return new ResultViewModel()
            {
                Message = "Updated Successfuuly",
                Success = true,
                Data = null
            };
        }


        public ResultViewModel Getfororder(string userid)
        {
            var result = CartRepo.getForOrder(userid);
            return new ResultViewModel
            {
                Data = result,
                Message = "",
                Success = true,

            };
        }


        public ResultViewModel GetByID(int id)
        {
            var result = CartRepo.GetById(id);
            return new ResultViewModel
            {
                Data = result,
                Message = "",
                Success = true,

            };

        }

    }
}
