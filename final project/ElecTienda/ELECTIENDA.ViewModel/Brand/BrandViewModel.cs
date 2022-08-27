using FirstLyer.Model;

namespace ELECTIENDA.ViewModel
{
    public static partial class BrandExtensions
    {
        public static BrandViewModel ToViewModel(this Brand model)
        {
            return new BrandViewModel
            {
                ID = model.ID,
                Name = model.Name
            };
        }
    }
    public class BrandViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
