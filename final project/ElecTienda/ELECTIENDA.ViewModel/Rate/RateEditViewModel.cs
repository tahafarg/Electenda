
using FirstLyer.Model;

namespace ELECTIENDA.ViewModel

{
    public static partial class RateExtention { 
    public static Rate ToModel(this RateEditViewModel model)
    {
        return new Rate
        {
            ID = model.ID,
            Rating = model.Rating,
            Review = model.Review,
        };
    }
    }
    public class RateEditViewModel
    {
            public int ID { get; set; }
            public string Review { get; set; }
            public decimal Rating { get; set; } = 0;

        
    }

}