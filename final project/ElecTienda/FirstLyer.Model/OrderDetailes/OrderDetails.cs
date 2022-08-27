using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstLyer.Model
{
    public class OrderDetails
    {
        public int ID { get; set; }
        public int? ServicesId { get; set; }

        public int? ProductId { get; set; }  
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        public float SubPrice { get; set; }
        public bool IsDeleted { get; set; }=false;
        public IsAccepted IsAccepted { get; set; } = IsAccepted.Bending;

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
        public virtual Services Services { get; set; } 
        
    }
}
