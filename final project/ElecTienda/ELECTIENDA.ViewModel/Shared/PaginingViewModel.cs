using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELECTIENDA.ViewModel
{
    public class PaginingViewModel<t>
    {
        public int pageindex;

        public int Count { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public t Data { get; set; }

    }
}
