using FirstLyer.Model;
using System.ComponentModel.DataAnnotations;


namespace ELECTIENDA.ViewModel
{
   
    public class OrderAddViewModel
    {
        public int ID { get; set; }
        public string UserID { get; set; }

        //[Required]
        public statues Statue { get; set; }

        //public int OrderNum { get; set; }
        //[Required]
        public DateTime OrderDate { get; set; }

       // [Required]
        public DateTime ShippingDate { get; set; }

       // [Required]
    //    public int Amount { get; set; }
     public float TotalPrice { get; set; }
        public List<OrderDetailsAddViewModel>? Details { get; set; }


    }
}
