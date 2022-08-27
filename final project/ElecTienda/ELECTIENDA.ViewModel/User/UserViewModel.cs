using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstLyer.Model;

namespace ELECTIENDA.ViewModel
{
   
    public class UserViewModel
    {
        
        public string ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string ImgSrc { get; set; }
        public int? ProviderId { get; set; }
    //    public List<string> phones { get; set; }
   //   public IsAccepted IsAccepted { get; set; } = IsAccepted.Bending;
        public bool IsDeleted { get; set; }

    }
}
