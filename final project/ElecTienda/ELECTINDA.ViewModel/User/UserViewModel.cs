using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstLyer.Model;

namespace ELECTINDA.ViewModel
{
   
    public class UserViewModel
    {
        
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string ImgSrc { get; set; }
        public List<string> phones { get; set; } 
    }
}
