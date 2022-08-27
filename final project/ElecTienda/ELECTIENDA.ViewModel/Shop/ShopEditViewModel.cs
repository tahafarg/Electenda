using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstLyer.Model;
using Microsoft.AspNetCore.Http;

namespace ELECTIENDA.ViewModel
{


    public static class ShopExtentionToAdd
    {
        public static Shop ToModel(this ShopEditViewModel model)
        {
            return new Shop
            {
                ID = model.Id,
                Name = model.Name,
                Address = model.Address,
                ImgSrc = model.ImageUrl,
                ProviderId = model.ProviderId,  

            };
        }
    }

        public class ShopEditViewModel
        {

            public int Id { get; set; }
            
            [Required(ErrorMessage = "name must have a value")]
            [StringLength(500, MinimumLength = 3)]
            public string? Name { get; set; }
            [Required(ErrorMessage = "Address must have a value")]
            [StringLength(500, MinimumLength = 3)]
            public string? Address { get; set; }
            [Required(ErrorMessage = "image must have a value")]
            public IFormFile? ImgSrc { get; set; }
            public string? ImageUrl { get; set; }
        [Required(ErrorMessage = "provider must have a value")]
            public int ProviderId { get; set; }
        }
    }

