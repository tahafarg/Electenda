using FirstLyer.Model;

namespace ELECTIENDA.ViewModel
{

    public enum statues
    {
        Pending, Done, Canceled
    }
    public class OrderViewModel
    {
        public int ID { get; set; }
        

        //public int OrderNum { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ShippingDate { get; set; }
    //    public int Amount { get; set; }
        public statues Statue { get; set; } 

        public string UserName { get; set; }
        public string UserAddress { get; set; }
       // public float SupPrice { get; set; }
       // public string? ProductName { get; set; }
       // public string? ServicesName { get; set; }
       // public int Quntity { get; set; }
        public float TotalPrice { get; set; }
    //    public List<float> OrderDetails { get; set; }
        public List<OrderDetailsViewModel>? Details { get; set; }


        //public string? Product { get; set; }
        //public string? Service { get; set; }
        //public string? Quntity { get; set; }
        //public string? Price { get; set; }
        


    }


}
