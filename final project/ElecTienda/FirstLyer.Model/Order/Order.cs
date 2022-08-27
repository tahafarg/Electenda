using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstLyer.Model
{
    public enum statues
    {
         Pending , Done , Canceled
    }
    public class Order
    {
        public int ID { get; set; }
        public string? UserID { get; set; }

        //public int OrderNum { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ShippingDate { get; set; }
       public float TotalPrice { get; set; }
       // public float Balance { get; set; }
        public statues Statue { get; set; }
        public bool IsDeleted { get; set; }=false;

        public virtual List<OrderDetails>? OrderDetailes { get; set; }
        public virtual User? User { get; set; }

    }
}
