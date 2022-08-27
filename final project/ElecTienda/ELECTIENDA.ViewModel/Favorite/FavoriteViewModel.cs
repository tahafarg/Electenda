using FirstLyer.Model;

namespace ELECTIENDA.ViewModel
{
  public class FavoriteViewModel
    {

        public int ID { get; set; }
        public string UserID { get; set; }
        public int? ServicesId { get; set; }
        public int? ProductID { get; set; }
         public string Name { get; set; }
         public float Price { get; set; }
       // public string? ProductName { get; set; }
       // public string? ServiceName { get; set; }
        //public float? ProductPrice { get; set; }
        //public float ServicesPrice { get; set; }
        public string? Img { get; set; }


    }
}


