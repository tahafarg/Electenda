using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ELECTIENDA.ViewModel
{
    public class OrderDetailsViewModel
    {
        public int ID { get; set; }
        public int OrderID { get; set; }
        public string? ProductName { get; set; }
        public int Quntity { get; set; }
        public string UserName { get; set; }
        public string UserAddress { get; set; }
        public float TotalPrice { get; set; }
        public string? Service { get; set; }
        public int Quantity { get; set; }
        public float SupPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ShippingDate { get; set; }

        public statues Statue { get; set; }



    }
}
