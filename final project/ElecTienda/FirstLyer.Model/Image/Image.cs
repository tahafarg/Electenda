using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstLyer.Model
{
    public class Image
    {
        public int ProductID { get; set; }
        public string Src { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Product? Product { get; set; }

    }
}
