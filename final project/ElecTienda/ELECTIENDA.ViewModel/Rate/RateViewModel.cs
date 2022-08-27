using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstLyer.Model;

namespace ELECTIENDA.ViewModel
{
    public static class RateExtetion
    {
        public static RateViewModel ToViewModel(this Rate model)
        {
            return new RateViewModel
            {
                ID = model.ID,
                Rating = model.Rating,
                Review = model.Review,
                Statues = model.IsAccepted.ToString(),
                UserName = model.User.FirstName,
                ProductName= model.Product.Name,
                ServicesName=model.Services.Name

            };
        }
    }

    public class RateViewModel
    {

        public int ID { get; set; }
        public string Review { get; set; }
        public decimal Rating { get; set; } = 0;

        public string Statues { get; set; }
        public string UserName { get; set; }
        public string ProductName { get; set; }
        public string ServicesName { get; set; }


    }
}
