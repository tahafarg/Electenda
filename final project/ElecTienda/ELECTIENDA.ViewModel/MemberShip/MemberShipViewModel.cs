using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstLyer.Model;

namespace ELECTIENDA.ViewModel
{
    public static class MemberShipExtetion{
    public static MemberShipViewModel ToViewModel( this Membership model)
        {
            return new MemberShipViewModel
            {
                ID = model.ID,
                Role=model.Type,
                DurartionTime=model.DurationOnDays,
                Price=model.Price,

            };
        }
}
    public class MemberShipViewModel 
    {
        public int ID { get; set; }
        public string Role { get; set; }

        public int DurartionTime { get; set; }
        public float Price { get; set; }




    }
}
