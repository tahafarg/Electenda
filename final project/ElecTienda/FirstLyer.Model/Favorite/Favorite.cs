using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstLyer.Model
{
    public class Favorite
    {
        public int ID { get; set; }
        public int? ServicesId { get; set; }
        public string  UserID { get; set; }
        public int? ProductID { get; set; }
        public bool IsDeleted { get; set; }=false;
        public IsAccepted IsAccepted { get; set; } = IsAccepted.Bending;

        public virtual Product Product { get; set; }
        public virtual Services Services { get; set; }
        public virtual User User { get; set; }



    }
}
