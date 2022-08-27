using Microsoft.AspNetCore.Mvc;
using ELECTIENDA.Repository;
using ELECTIENDA.ViewModel;




namespace ElECTIENDA.MVC
{
    public class OrderController : ControllerBase
    {
        private readonly OrderRepository orderRep;
        private readonly UserRepository userRep;
        private readonly UnitOfWork unitOfWork;


        public OrderController(OrderRepository _ordRep , UnitOfWork _unitOfWork, UserRepository _userRep)
        {
            orderRep = _ordRep;
            unitOfWork = _unitOfWork;
            userRep = _userRep;
        }


       //// public IActionResult Get( DateTime orderDate , DateTime shippingDate , string orderBy = "ID", bool isAscending = false
       ////     , int pageIndex = 1,int pageSize = 10)
       //// {
       ////     var data =orderRep.Get( orderDate , shippingDate ,orderBy, isAscending, pageIndex, pageSize);
       ////     return View(data);

       ////}

        //public IActionResult search(int pageIndex = 1 , int pageSize = 2 )
        //{
        //    var data =orderRep.Search(pageIndex, pageSize );
        //    return View(data);
        //}

        //public IActionResult Search(DateTime orderDate, DateTime shippingDate, string orderBy = "ID", bool isAscending = false
        //    , int pageIndex = 1, int pageSize = 10)
        //{
        //    var data = orderRep.Search2(orderDate , shippingDate ,orderBy, isAscending, pageIndex, pageSize);
        //    return View(data);
        //}

        //public IActionResult ProviderSearch(DateTime orderDate, DateTime shippingDate, string orderBy = "ID", bool isAscending = false
        //    , int pageIndex = 1, int pageSize = 10)
        //{
        //    var data = orderRep.Search2(orderDate, shippingDate, orderBy, isAscending, pageIndex, pageSize);
        //    return View(data);
        //}

        [HttpGet]
        public IActionResult Add()
        {
            return null;
        }

        [HttpPost]

        public IActionResult Add(OrderAddViewModel model)
        {
            if (ModelState.IsValid == true)
            { 
                orderRep.Add(model);
                unitOfWork.Save();
                return null;
            }

            else
            {
                ModelState.AddModelError("", "Please check your data");
                return null;
            }
        }

        [HttpGet]

        public IActionResult Update( int id)
        {
             var result = orderRep.GetById(id);
            OrderAddViewModel model = new OrderAddViewModel()
            {
                ID = result.ID, 
         //       Amount = result.Amount,
                OrderDate=result.OrderDate,
                ShippingDate=result.ShippingDate,
            };
            return null;
        }

        [HttpPost]

        public IActionResult Update(int id , OrderAddViewModel addModel)
        {

            if(ModelState.IsValid == true)
            { 
            orderRep.Update(id , addModel);
                unitOfWork.Save();
            return RedirectToAction("Search");
            }

            else
                return null;
        }


        //public IActionResult Cancel(int ID)
        //{
        //    var result = orderRep.Cancele(ID);
        //    unitOfWork.Save();
        //    return RedirectToAction("Search");
        //}


        //public IActionResult Done(int ID)
        //{
        //    var result = orderRep.Done(ID);
        //    unitOfWork.Save();
        //    return RedirectToAction("Search");
        //}

    }
    }
