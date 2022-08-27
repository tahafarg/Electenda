
using FirstLyer.Model;

namespace ELECTIENDA.ViewModel
{
 
    public class FavoriteEditViewModel
    {
        public int ID { get; set; }
        public string UserID { get; set; }

        public int? ServicesId { get; set; }
        public int? ProductID { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }

    }
}

