using FirstLyer.Model;

namespace ELECTIENDA.ViewModel
{
    public static partial class BrandExtensions
    {
        public static Brand ToModel(this BrandEditViewModel model)
        {
            return new Brand
            {
                ID = model.ID,
                Name = model.Name
            };
        }
    }
    public class BrandEditViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

}
