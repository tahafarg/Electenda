using FirstLyer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELECTIENDA.ViewModel
{
    public static class UserExtensions
    {
        public static User ToModel(this UserEditViewModel model)
        {
            return new User
            {

                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email,
                Address = model.Address,
               // ImgSrc = model.Image
        //        PhoneNumber = model.PhoneNumber,
            };
        }

        public static UserViewModel ToViewModel(this User model)
        {
            return new UserViewModel
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Address = model.Address,
                //ImgSrc = model.ImgSrc

            };
        }


    }
}
