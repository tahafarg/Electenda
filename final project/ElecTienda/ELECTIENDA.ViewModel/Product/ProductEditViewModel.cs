using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace ELECTIENDA.ViewModel
{
    public class ProductEditViewModel
    {
       
            public string Name { get; set; }
            public string Description { get; set; }
            public float Price { get; set; }
            public bool isActive { get; set; }
            public int Quantity { get; set; }
            public string Color { get; set; }
            public int ID { get; set; }
            public int ProviderID { get; set; }
            public int CategoryID { get; set; }

           public List<string>? ImagesUrls { get; set; }
           public IFormFileCollection? Images { get; set; }
    }

    
}
