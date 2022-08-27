using FirstLyer.Model;
using System.ComponentModel.DataAnnotations;

namespace ELECTIENDA.ViewModel
{
    public static class ServicesExtentionToAdd 
    {
        public static Services Model (this ServicesAddViewModel model)
        {
            return new Services
            {
                ID = model.Id,
                Name=model.Name,
                Description=model.Description,
                Price=model.Price,
                StartDate=model.StartDate,
                EndDate=model.EndDate,
                IsActive=model.IsActive,
                CategoryID=model.CategoryId,
                ProviderID=model.ProviderId
            };
        }
    }
    public class ServicesAddViewModel
    {
       
        public int Id { get; set; }
        [Required (ErrorMessage ="name must have a value")]
        [StringLength (500,MinimumLength =3)]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Description must have a value")]
        [StringLength(500, MinimumLength = 6)]
        public string? Description { get; set; }
        [Required(ErrorMessage = "price must have a value")]
        public float Price { get; set; }
        [Required(ErrorMessage = "Start Date must have a value")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "End DAte must have a value")]
        public DateTime EndDate { get; set; }


        // public string? ProviderName { get; set; }
        [Required]
        [Display(Name = "Choose a Category")]
        public int CategoryId { get; set; }

        [Required]
        public bool IsActive { get; set; }
        public int ProviderId { get; set; }

    }
}
