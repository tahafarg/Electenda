using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ElECTIENDA.MVC
{
   public class HomeController : Controller
    {

        public ViewResult Index()
        {
            ViewBag.Title = "Home";
            return View();
        }

        public ViewResult Contactus()
        {
            return View();
        }

        public ViewResult Aboutus()
        {
            return View();
        }

    }
}
