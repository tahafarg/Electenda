using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstLyer.Model
{
    public class Category
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImgSrc { get; set; }
        public bool IsDeleted { get; set; }=false;
        public IsAccepted IsAccepted { get; set; } = IsAccepted.Bending;
        public virtual List <CategoryBrand> CategoryBrands { get; set; }
        public virtual List<Product> Products { get; set; }
        public virtual List<Services> Services { get; set; }



    }
}
