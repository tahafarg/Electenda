using FirstLyer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELECTIENDA.ViewModel
{
    public class CartViewModel
    {

        public int ID { get; set; }
        public string UserID { get; set; }
        public int? ServicesId { get; set; }
        public int? ProductID { get; set; }

        public int Quantity { get; set; }
        public string Name { get; set; } 
        public float Price { get; set; } 
        public string Src { get; set; }
    }
}


