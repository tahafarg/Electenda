using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstLyer.Model;
using Microsoft.AspNetCore.Http;

namespace ELECTIENDA.ViewModel
{
    public static class MemberShipExtetionToEdit
    {
        public static Membership ToModel(this MemberShipEditViewModel model)
        {
            return new Membership
            {
                ID = model.ID,
                Type=model.Role,
                DurationOnDays =model.DurartionTime,
                Price=model.Price,
                
            };
        }
    }
    public class MemberShipEditViewModel
    {
        public int ID { get; set; }
        public string Role { get; set; }
        
        public int DurartionTime { get; set; }
        public float Price { get; set; }


    }
}
