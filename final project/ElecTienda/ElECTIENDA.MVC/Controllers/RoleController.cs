using ELECTIENDA.Repository;
using Microsoft.AspNetCore.Mvc;
using ELECTIENDA.ViewModel;

namespace ElECTIENDA.MVC
{
    public class RoleController : Controller
    {
        RoleRepository roleRep;
        public RoleController(RoleRepository _roleRep )
        {
            roleRep = _roleRep;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }


        [HttpPost]

        public async Task<IActionResult> Add(RoleAddViewModel model)
        {
            var result = await roleRep.Add(model);

        if(result.Succeeded)
            {
                ModelState.Clear();
                return View();
            }
            
           return View();
        }


    }
}
