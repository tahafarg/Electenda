using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstLyer.Model;

namespace ELECTIENDA.ViewModel
{
    public static class CategoryExtetion{
    public static CategoryViewModel ToViewModel( this Category model)
        {
            return new CategoryViewModel
            {
                ID = model.ID,
                Name=model.Name,
                Description=model.Description,
                Imgsrc= model.ImgSrc
                

            };
        }
}
    public class CategoryViewModel 
    {
        public int ID { get; set; }
        public string Name { get; set; }    
        public string Description { get; set; }
        public string Imgsrc { get; set; }= string.Empty;

        public bool IsDeleted { get; set; }= false;

        public List<string> ServciesName { get; set; }
        public List<string>  productname { get; set; }




    }
}
