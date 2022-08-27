using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstLyer.Model
{
    public class Membership
    {
        public int ID { get; set; }
        public string Type { get; set; }
        public bool IsDeleted { get; set; }=false;
        public IsAccepted IsAccepted { get; set; } = IsAccepted.Bending;
        public int DurationOnDays { get; set; }

        public float Price { get; set; }

        public virtual List<Provider> Providers { get; set; }   

    }
}
