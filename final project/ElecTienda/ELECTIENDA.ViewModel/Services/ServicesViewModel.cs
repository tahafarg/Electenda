using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstLyer.Model;

namespace ELECTIENDA.ViewModel
{
    public static class ServicesExtantion
    {
        public static ServicesViewModel ToViewModel(this Services model)
        {
            return new ServicesViewModel
            {
                ID = model.ID,
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                IsActive = model.IsActive,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Src = model.Src,

                //CategoryName = model.Category.Name,
                //ProdviderName=model.Provider.User.FirstName +" " + model.Provider.User.LastName,


            };
        }

    }
    public class ServicesViewModel
    {

        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } 
        public float Price { get; set; }
        public bool IsActive { get; set; } = false;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Src { get; set; }
        
        public bool IsDeleted { get; set; }= false;

        public string CategoryName { get; set; }
        public string ProdviderName { get; set; }
        
        public int Provider_id { get; set; }


    }
}

