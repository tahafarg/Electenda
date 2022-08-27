using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstLyer.Model
{
    public class CategoryBrand
    {
         public int BrandId { get; set; }
         public int CategoryId { get; set; }
        public bool IsDeleted { get; set; }=false;
        public IsAccepted IsAccepted { get; set; } = IsAccepted.Bending;

        public virtual Brand Brand { get; set; }
        public virtual Category Category { get; set; }

    }
}
