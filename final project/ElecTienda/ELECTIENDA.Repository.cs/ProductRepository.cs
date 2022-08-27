using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstLyer.Model;
using LinqKit;
using ELECTIENDA.ViewModel;
using X.PagedList;
using Microsoft.AspNetCore.Http;

namespace ELECTIENDA.Repository
{
    public class ProductRepository : GeneralRepository<Product>
    {

        public ProductRepository(FinalProjectContext _dbContext) : base(_dbContext)
        { }
public PaginingViewModel<List<ProductViewModel>> GetProducts(int id , int ProviderId = 0, string providerName = null, string categoryName = null, float price = 0, int catId = 0, string name = null , string color = null, string orderBy = null, bool isAscending = false, int pageIndex = 1, int pageSize = 20)
        {
            var filter = PredicateBuilder.New<Product>();
            var oldFilter = filter;
            if(id>0)
                filter = filter.Or(p => p.ID== id);
            if (catId > 0)
                filter = filter.Or(p => p.CategoryID == catId);
            if (!string.IsNullOrEmpty(name))
                filter = filter.Or(i => i.Name.Contains(name));
            if (price > 0.0)
                filter = filter.Or(i => i.Price == price);
            if (ProviderId > 0)
                filter = filter.Or(i => i.ProviderID == ProviderId);
            if (!string.IsNullOrEmpty(color))
                filter = filter.Or(i => i.Color.Contains(color));

            if (!string.IsNullOrEmpty(providerName))
                filter = filter.Or(i => i.Provider.User.FirstName.Contains(providerName) || i.Provider.User.LastName.Contains(providerName));
           
            if (!string.IsNullOrEmpty(categoryName))
                filter = filter.Or(i => i.Category.Name.Contains(categoryName));

            if (filter == oldFilter)
                filter = null;

            var query = base.Get(filter, orderBy, isAscending, pageIndex, pageSize, null);


            var Result =
            query.Where(i => i.IsDeleted == false).Select(i => new ProductViewModel
            {
                ID = i.ID,
                Name = i.Name,
                color = i.Color,
                categoryName = i.Category.Name,
                providerName = i.Provider.User.FirstName + "" + i.Provider.User.LastName,
                isActive = i.IsActive,
                Quantity = i.Quantity,
                price = i.Price,
                status = i.IsAccepted.ToString(),
                Imgs = i.Images.Select(i => i.Src).ToList()

            });


            PaginingViewModel<List<ProductViewModel>>
                finalResult = new PaginingViewModel<List<ProductViewModel>>
                {
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    Count = base.GetList().Count(),
                    Data = Result.ToList()

                };

            return finalResult;

        }

        public IPagedList<ProductViewModel> Search(int id, int ProviderId = 0, int catId = 0, string name = null, float price = 0, string color = null, string orderBy = null, bool isAscending = false, int pageIndex = 1, int pageSize = 20)
        {
            var filter = PredicateBuilder.New<Product>();
            var oldFilter = filter;
            if (id > 0)
                filter = filter.Or(p => p.ID == id);
            if (catId > 0)
                filter = filter.Or(p => p.CategoryID == catId);
            if (!string.IsNullOrEmpty(name))
                filter = filter.Or(i => i.Name.Contains(name));
            if (price > 0.0)
                filter = filter.Or(i => i.Price == price);
            if (ProviderId > 0)
                filter = filter.Or(i => i.ProviderID == ProviderId);
            if (!string.IsNullOrEmpty(color))
                filter = filter.Or(i => i.Color.Contains(color));

            if (filter == oldFilter)
                filter = null;
            var data = base.Get(filter, orderBy, isAscending, pageIndex, pageSize, null);

            var paginatedProducts = data.Where(i => i.IsDeleted == false).Select(i => new ProductViewModel
            {
                ID = i.ID,
                Name = i.Name,
                color = i.Color,
                categoryName = i.Category.Name,
                providerName = i.Provider.User.FirstName + "" + i.Provider.User.LastName,
                isActive = i.IsActive,
                Quantity = i.Quantity,
                price = i.Price,
                status = i.IsAccepted.ToString(),
                Imgs = i.Images.Select(i => i.Src).ToList()


            }).ToPagedList(pageIndex, pageSize);
            return paginatedProducts;
        }

        public void productAccept(int _id)
        {
            var data = base.GetList().Where(i => i.ID == _id).FirstOrDefault();
            data.IsAccepted = IsAccepted.Accepted;
            
            base.Update(data);
        }

        public ProductViewModel Add(ProductEditViewModel model)
        {
            Product product = model.ToModel();
            return base.Add(product).Entity.ToViewModel();
        }

        public ProductViewModel ProductAccepted(int _id)
        {
            var data = base.GetList().Where(i => i.ID == _id).FirstOrDefault();
            data.IsAccepted = IsAccepted.Accepted;

            return base.Update(data).Entity.ToViewModel();

        }

        public ProductViewModel ProductRejected(int _id)
        {
            Product? data = base.GetList().Where(i => i.ID == _id).FirstOrDefault();
            data.IsAccepted = IsAccepted.Rejected;

            return base.Update(data).Entity.ToViewModel();

        }

        public ProductViewModel Remove(int _id)
        {
            var data = base.GetList().Where(i => i.ID == _id).FirstOrDefault();
            data.IsDeleted = true;
            return base.Update(data).Entity.ToViewModel();

        }

        //public ProductViewModel Delete(int _id)
        //{
        //    var data = base.GetList().Where(i => i.ID == _id).FirstOrDefault();
        //    return base.Remove(data).Entity.ToViewModel();

        //}

        public ProductEditViewModel GetById(int _id)
        {
            return base.GetList().Where(i => i.ID == _id).Select(i => new ProductEditViewModel
            {
                ID = i.ID,
                Name = i.Name,
                Description = i.Description,
                Price = i.Price,
                isActive = i.IsActive,
                Quantity = i.Quantity,
                Color = i.Color,
                ProviderID = i.ProviderID,
                CategoryID = i.CategoryID,
                Images = (IFormFileCollection)i.Images.Select(i => i.Src).ToList()

            }).FirstOrDefault();

        }

        public ProductEditViewModel GetByIdForBalance(int? _id)
        {
            return
            base.GetList().Where(i => i.ID == _id).Select(i => new ProductEditViewModel
            {
                ID = i.ID,
                Name = i.Name,
                Description = i.Description,
                Price = i.Price,
                isActive = i.IsActive,
                Quantity = i.Quantity,
                Color = i.Color,
                ProviderID = i.ProviderID,
                CategoryID = i.CategoryID,
      //          Images = (Microsoft.AspNetCore.Http.IFormFileCollection)i.Images.Select(i => i.Src).ToList()

            }).FirstOrDefault();

        }
        public ProductViewModel Update(ProductEditViewModel model)
        {
            Product product = model.ToModel();
            return base.Update(product).Entity.ToViewModel();
        }

        public void UpdateQuantity(int? prodId , int q)
        {
            var res = base.GetList().Where(p => p.ID == prodId && p.IsDeleted == false).FirstOrDefault();
            res.Quantity = res.Quantity - q;
            base.Update(res);


        }

        public ProductViewModel GetByIdapi(int _id)
        {
            return base.GetList().Where(i => i.ID == _id).Select(i => new ProductViewModel
            {

                ID = i.ID,
                Name = i.Name,
                color = i.Color,
                categoryName = i.Category.Name,
                providerName = i.Provider.User.FirstName + " " + i.Provider.User.LastName,
                Description = i.Description,
                Quantity = i.Quantity,
                price = i.Price,
                status = i.IsAccepted.ToString(),
                Imgs = i.Images.Select(i => i.Src).ToList()

            }).FirstOrDefault();

        }

    }
}
