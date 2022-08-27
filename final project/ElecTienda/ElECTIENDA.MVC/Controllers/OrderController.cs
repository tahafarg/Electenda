using Microsoft.AspNetCore.Mvc;
using ELECTIENDA.Repository;
using ELECTIENDA.ViewModel;
using System.Security.Claims;

namespace ElECTIENDA.MVC
{
    public class OrderController : Controller
    {
        private readonly OrderRepository orderRep;
        private readonly UserRepository userRep;
        private readonly UnitOfWork unitOfWork;
        private readonly CartRepository cartRep;
        private readonly OrderDetailsRepository orderDetailsRepo;
        private readonly ProviderRepository proprepo;
        private readonly ProductRepository productRep;


        public OrderController(OrderRepository _ordRep,ProviderRepository prop, CartRepository _CartRepo, UnitOfWork _unitOfWork,
            UserRepository _userRep, OrderDetailsRepository orderDetailsrepo, ProductRepository _productRep)
        {
            orderDetailsRepo = orderDetailsrepo;
            cartRep = _CartRepo;
            orderRep = _ordRep;
            unitOfWork = _unitOfWork;
            userRep = _userRep;
            proprepo = prop;
            productRep = _productRep;
        }


        public ResultViewModel Get( DateTime orderDate , DateTime shippingDate , string orderBy = "ID", bool isAscending = false
            , int pageIndex = 1,int pageSize = 10)
        {
            var data =orderRep.Get( orderDate , shippingDate ,orderBy, isAscending, pageIndex, pageSize);
            return new ResultViewModel()
            {
                Message = "Added Successfuuly",
                Success = true,
                Data = data
            };

        }

        public IActionResult AdminSearch( int pageIndex = 1, int pageSize = 10)
        {
            var clamisidentity = (System.Security.Claims.ClaimsIdentity)this.User.Identity;
            var claim = clamisidentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            var UserId = claim.Value;
            int ProviderId = userRep.Detailes(UserId);
            ViewBag.Provider = proprepo.getproviderbyUserID(UserId);
            var data = orderDetailsRepo.GetProviderOrder(ProviderId,pageIndex, pageSize);
            return View(data);
        }

        public ResultViewModel Search(DateTime orderDate, DateTime shippingDate, string orderBy = "ID", bool isAscending = false
            , int pageIndex = 1, int pageSize = 10)
        {
            var data = orderRep.Search2(orderDate , shippingDate ,orderBy, isAscending, pageIndex, pageSize);
            return new ResultViewModel()
            {
                Message = "Added Successfuuly",
                Success = true,
                Data = data
            };
        }

        public IActionResult ProviderSearch(DateTime orderDate, DateTime shippingDate, string orderBy = "ID", bool isAscending = false
            , int pageIndex = 1, int pageSize = 10)
        {
            
            var data = orderRep.Search2(orderDate, shippingDate, orderBy, isAscending, pageIndex, pageSize);
            return View(data);
        }

        //[HttpGet]
        //public IActionResult Add()
        //{
        //    return View();
        //}

        [HttpPost]

        public IActionResult Add2(OrderAddViewModel model)
        {
            if (ModelState.IsValid == true)
            {
                orderRep.Add(model);
               // cartRep.Add(model);
                unitOfWork.Save();
                return View("Add");
            }

            else
            {
                ModelState.AddModelError("", "Please check your data");
                return View();
            }
        }

        [HttpPost]
        public ResultViewModel Add(string UserId)
        {
            try
            {

                //var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                OrderAddViewModel order=new OrderAddViewModel();
                order.OrderDate = DateTime.Now;
                order.ShippingDate = DateTime.Now;
                order.UserID = UserId;
                order.Statue = statues.Pending;


                List<OrderDetailsAddViewModel> details = new List<OrderDetailsAddViewModel>();
                var cartItems = cartRep.getForOrder(UserId);
                if (cartItems.Count()>0)
                {
                    order.TotalPrice = 0;
                    foreach (var cartItem in cartItems)
                    {
                        details.Add( new OrderDetailsAddViewModel {
                            ProductId = cartItem.ProductID,
                            ServicesId = cartItem.ServicesId,
                            Quantity = cartItem.Quantity,
                            SupPrice = cartItem.Price * cartItem.Quantity
                        });
                        cartRep.Remove(cartItem);
                        productRep.UpdateQuantity(cartItem.ProductID, cartItem.Quantity);
                        proprepo.Balance(cartItem.ProductID, cartItem.ServicesId, cartItem.Price * cartItem.Quantity );
                        order.TotalPrice = order.TotalPrice + cartItem.Price * cartItem.Quantity;
                        
                    }
                }
                order.Details = details;    
                var res = orderRep.Add(order);
                
                unitOfWork.Save();
                return new ResultViewModel()
                {
                    Message = "Added sucessful",
                    Data = null,
                    Success = true

                };
            }
            catch(Exception ex)
            {
                return new ResultViewModel()
                {
                    Message = "Error to Add",
                    Data = ex,
                    Success = false,
                };
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
            return View(model);
        }

        [HttpPost]

        public IActionResult Update(int id , OrderAddViewModel addModel)
        {

            if(ModelState.IsValid == true)
            { 
            orderRep.Update(id , addModel);
                unitOfWork.Save();
            return RedirectToAction("ProviderSearch");
            }

            else
                return View();
        }


        public IActionResult Cancel(int ID)
        {
            var result = orderRep.Cancele(ID);
            unitOfWork.Save();
            return RedirectToAction("ProviderSearch");
        }


        public IActionResult Done(int ID)
        {
            var result = orderRep.Done(ID);
            unitOfWork.Save();
            return RedirectToAction("ProviderSearch");
        }


        public IActionResult Details(int id)
        {

            var clamisidentity = (System.Security.Claims.ClaimsIdentity)this.User.Identity;
            var claim = clamisidentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            var UserId = claim.Value;
            var result = orderRep.GetByIdForOrderDetailes(id);
            return View(result);
        }
    }
    }
