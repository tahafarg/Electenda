using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstLyer.Model
{
    public class UserPhone
    {
        public string? UserID { get; set; }
        public string? Phone { get; set; }
        public bool IsDeleted { get; set; }

        public virtual User? User { get; set; }
    }
}
