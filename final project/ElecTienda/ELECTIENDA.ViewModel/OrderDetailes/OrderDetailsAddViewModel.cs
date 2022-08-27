using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ELECTIENDA.ViewModel
{
    public class OrderDetailsAddViewModel
    {

        public int ID { get; set; }
        public int OrderID { get; set; }
        public int? ProductId { get; set; }
        public int? ServicesId { get; set; }
        public int Quantity { get; set; }
        public float SupPrice { get; set; }

    }
}
