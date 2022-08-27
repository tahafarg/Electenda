using Microsoft.AspNetCore.Mvc;
using ELECTIENDA.Repository;
using ELECTIENDA.ViewModel;

namespace ElECTIENDA.MVC
{
    public class OrderDetailsController : Controller
    {

        private readonly OrderDetailsRepository orderDetailsRepo;
        private readonly UnitOfWork unitOfWork;


        public OrderDetailsController(OrderDetailsRepository _ordDetailsRepo, UnitOfWork _unitOfWork)
        {
            orderDetailsRepo = _ordDetailsRepo;
            unitOfWork = _unitOfWork;
        }


        public IActionResult Get(int id = 0, float TotalPrice = 0, string orderBy = null, bool isAscending = false, int pageIndex = 1, int pageSize = 20)
        {
            var data =
           orderDetailsRepo.GetOrderDetails(id, TotalPrice, orderBy, isAscending, pageIndex, pageSize);
            return View(data);
        }
    }
}
