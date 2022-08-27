using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using FirstLyer.Model;



namespace ELECTIENDA.ViewModel
{
   
    public class ProductViewModel
    {
        public int? ID { get; set; }
        public string Name { get; set; }
        public double price { get; set; }
        public string categoryName { get; set; }
        public string providerName { get; set; }
        public string Description { get; set; }

        public bool isActive { get; set; }
        public string color { get; set; }
        public int Quantity { get; set; }
        public string status { get; set; }
        public List<string> Imgs { get; set; }
       // public string Imgs { get; set; }
    }

}
