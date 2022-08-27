using FirstLyer.Model;
namespace ELECTIENDA.ViewModel
{
    public static class ShopExtantion
    {
        public static ShopViewModel ToViewModel(this Shop model)
        {
            return new ShopViewModel
            {
                ID = model.ID,
                Name = model.Name,
                Address=model.Address,
                ImgSrc=model.ImgSrc,
                Providerid = model.ProviderId
            };
        }

    }
    public class ShopViewModel
    {
        public int ID { get; set; }
        public int Providerid { get; set; }
        public string? status { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? ImgSrc { get; set; }
        public bool IsDeleted { get; set; }
        
    }
}
