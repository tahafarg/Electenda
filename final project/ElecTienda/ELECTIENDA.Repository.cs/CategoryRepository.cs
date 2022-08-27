using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ELECTIENDA.ViewModel;
using X.PagedList;
using FirstLyer.Model;
using LinqKit;

namespace ELECTIENDA.Repository
{
    public class CategoryRepository : GeneralRepository<Category>
    {
        public CategoryRepository(FinalProjectContext _context) : base(_context)
        { }
        public PaginingViewModel<List<CategoryViewModel>> Get(string name = "",
            string orderBy = "ID", bool isAscending = false, int pageIndex = 1,
                    int pageSize = 20)
        {
            var filter = PredicateBuilder.New<Category>();
            var OldFilter = filter;
            if (!string.IsNullOrEmpty(name))
                filter = filter.Or(i => i.Name.Contains(name));

            if (OldFilter == filter)
                filter = null;
            var Query = base.Get(filter, orderBy, isAscending, pageIndex, pageSize);
            var Result
                = Query.Where(i=>i.IsDeleted==false).Select(i => new CategoryViewModel
                {
                    ID = i.ID,
                    Name = i.Name,
                    Description = i.Description,
                    Imgsrc = i.ImgSrc,
                    productname = i.Products.Select(i => i.Name).ToList(),
                    ServciesName = i.Services.Select(i => i.Name).ToList()

                });
            PaginingViewModel<List<CategoryViewModel>>
                finalResult = new PaginingViewModel<List<CategoryViewModel>>()
                {
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    Count = base.GetList().Count(),
                    Data = Result.ToList()
                };


            return finalResult;
        }

        public IPagedList<CategoryViewModel> Search2(string name = "",
            string orderBy = "ID", bool isAscending = false, int pageIndex = 1,
                    int pageSize = 10)
        {
            var filter = PredicateBuilder.New<Category>();
            var OldFilter = filter;
            if (!string.IsNullOrEmpty(name))
                filter = filter.Or(i => i.Name.Contains(name));

            if (OldFilter == filter)
                filter = null;
            var query = base.Get(filter, orderBy, isAscending, pageIndex, pageSize);
            var result
                = query.Where(i => i.IsDeleted == false).Select(i => new CategoryViewModel
                {

                    ID = i.ID,
                    Name = i.Name,
                    Description = i.Description,
                    Imgsrc = i.ImgSrc,
                    productname = i.Products.Select(i => i.Name).ToList(),
                    ServciesName = i.Services.Select(i => i.Name).ToList()

                }).ToPagedList(pageIndex, pageSize);

            return result;
        }


        public CategoryViewModel? GetByID(int _Id)
        {
            return base.GetList()
            .Where(i => i.ID == _Id).Select(i => new CategoryViewModel
            {
                ID = i.ID,
                Name = i.Name,
                Description = i.Description,
                Imgsrc = i.ImgSrc,
                productname = i.Products.Select(i => i.Name).ToList(),
                ServciesName = i.Services.Select(i => i.Name).ToList()

            })?.FirstOrDefault();
        }

       
        public CategoryViewModel Add (CategoryEditViewModel model)
        {
            Category category = model.ToModel();
            return base.Add(category).Entity.ToViewModel();
        }

        public CategoryViewModel CategoryAccepted(int _id)
        {
            return base.isAccepted(_id).Entity.ToViewModel();
        }

        public CategoryViewModel UpdateCat (CategoryEditViewModel model)
        {
            Category services = model.ToModel();
            return base.Update(services).Entity.ToViewModel();
        }


        public CategoryViewModel CategoryRejected(int _id)
        {
            return base.isRejected(_id).Entity.ToViewModel();
        }

        public CategoryViewModel SoftDeleted(int id)
        {
            return base.isDeleted(id).Entity.ToViewModel();
        }

        public List<TextValueViewModel> GetDropDownValues() =>
           GetList().Select(i => new TextValueViewModel
           {
               Value = i.ID,
               Text = i.Name
           }).ToList();

    }
}
