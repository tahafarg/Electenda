using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstLyer.Model
{
    public class Shop
    {
        public int ID { get; set; }
        public int ProviderId { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? ImgSrc { get; set; }
        public bool IsDeleted { get; set; } = false;
        public IsAccepted IsAccepted { get; set; } = IsAccepted.Bending;

        public virtual Provider? Provider { get; set; }

    }
}
