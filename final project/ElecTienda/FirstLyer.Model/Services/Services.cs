using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstLyer.Model
{
    public class Services
    {
        public int ID { get; set; }
        public int ProviderID { get; set; }
        public int CategoryID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public float Price { get; set; }
        public bool IsActive { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate   { get; set; }
        public bool IsDeleted { get; set; }=false;
        public IsAccepted IsAccepted { get; set; } = IsAccepted.Bending;
        public string? Src { get; set; }

        public virtual Provider? Provider { get; set; }
        public virtual List<Cart>? Carts { get; set; }
        public virtual List<Rate>? Rates { get; set; }
        public virtual List<Favorite>? Favorites { get; set; }
        public virtual List<OrderDetails>? OrderDetailes { get; set; }
        public virtual Category? Category { get; set; }

    }
}
