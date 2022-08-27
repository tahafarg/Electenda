using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstLyer.Model;
using Microsoft.AspNetCore.Http;

namespace ELECTIENDA.ViewModel
{
    public static class CategoryExtetionToEdit
    {
        public static Category ToModel(this CategoryEditViewModel model)
        {
            return new Category
            {
                ID = model.ID,
                Name = model.Name,
                Description = model.Description,
                ImgSrc = model.Imgsrc
                
            };
        }
    }
    public class CategoryEditViewModel
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Imgsrc { get; set; } = string.Empty;

        public int servciesid { get; set; }
        public int productid { get; set; }




    }
}
