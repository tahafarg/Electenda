using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstLyer.Model;

namespace ELECTIENDA.ViewModel
{
    public static class ProviderExtensions
    {
        
        public static Provider ToProvModel(this ProviderAddViewModel model)
        {
            return new Provider
            {
                MembershipID = model.MembershipID,
                LicenseImg = model.LicenseImageUrl,
                

            };
        }

        public static User ToModel(this ProviderAddViewModel model)
        {
            return new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email,
               ImgSrc = model.ImageUrl,
               
              

            };
        }
        public static ProviderViewModel ToViewModel (this Provider provider)
        {
            return new ProviderViewModel
            {
                ID = provider.ProviderID,
                licenceImg=provider.LicenseImg,


            };
        }

    }
}
