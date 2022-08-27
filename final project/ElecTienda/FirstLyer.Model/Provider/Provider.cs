using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstLyer.Model
{
    public class Provider
    {
        public int ProviderID { get; set; }
        public string? UserID { get; set; }
        public int MembershipID { get; set; }
        public float Balance { get; set; }

        //public string ShopName { get; set; }
        public bool IsDeleted { get; set; }=false;
        public IsAccepted IsAccepted { get; set; } = IsAccepted.Bending;
        
        // public string ShopAddress { get; set; }
        // public string ShopImg { get; set; }
        public string? LicenseImg { get; set; }
        public virtual User? User { get; set; }
        public virtual Membership? Membership { get; set; }
        public virtual List<Product>? Products { get; set; }
        public virtual List<Services>? Services { get; set; }
        public virtual List<Shop>? Shops { get; set; }

    }
}
