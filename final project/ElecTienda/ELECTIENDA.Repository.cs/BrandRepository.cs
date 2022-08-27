using ELECTIENDA.ViewModel;
using FirstLyer.Model;
using LinqKit;
using X.PagedList;

namespace ELECTIENDA.Repository
{
    public class BrandRepository : GeneralRepository<Brand>
    {
        public BrandRepository(FinalProjectContext _context):base(_context)
        {

        }
        public PaginingViewModel<List<BrandViewModel>> Get(int ID = 0, string Name = "",
            string orderby = "ID", bool isAscending = false, int pageIndex = 1,
            int pageSize = 10)
        {
            var query = PredicateBuilder.New<Brand>();
            if (ID > 0)
                query = query.Or(p => p.ID == ID);
            if (!string.IsNullOrEmpty(Name))
                query = query.Or(p => p.Name.Contains(Name));

            var
                 filter = base.Get(query, orderby, isAscending, pageIndex, pageSize);
            var result =
            filter.Select(i => new BrandViewModel
            {
                ID = i.ID,
                Name = i.Name
            });
         

            PaginingViewModel<List<BrandViewModel>>
                finalResult = new PaginingViewModel<List<BrandViewModel>>()
                {
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    Count = base.GetList().Count(),
                    Data = result.ToList()
                };
            return finalResult;

        }


        public BrandViewModel GetbyID(int _Id)
        {
            return base.GetList().Where(i => i.ID == _Id).Select(i => new BrandViewModel
            {
                Name = i.Name,
                ID = i.ID
            }).FirstOrDefault();
        }

        public IPagedList<BrandViewModel> Search(int PageIndex=2,int PageSize = 1)=>
       
            GetList().Select( i=> new BrandViewModel
            {
                Name = i.Name,
                ID = i.ID
            }).ToPagedList(PageIndex,PageSize);

        public IPagedList<BrandViewModel> Search2(int ID = 0, string Name = "",
            string orderby = "ID", bool isAscending = false, int pageIndex = 1,
            int pageSize = 10)
        {
            var query = PredicateBuilder.New<Brand>();
            if (ID > 0)
                query = query.Or(p => p.ID == ID);
            if (!string.IsNullOrEmpty(Name))
                query = query.Or(p => p.Name.Contains(Name));

            var
                 filter = base.Get(query, orderby, isAscending, pageIndex, pageSize);
            var result
                = filter.Where(i => i.IsDeleted == false).Select(i => new BrandViewModel
                {

                    ID = i.ID,
                    Name = i.Name
                }).ToPagedList(pageIndex, pageSize);

            return result;
        }
        public BrandViewModel Add(BrandEditViewModel model)
        {
            Brand Brand = model.ToModel();
           return base.Add(Brand).Entity.ToViewModel();
        }


        public BrandViewModel SoftDeleted(int id)
        {
            return base.isDeleted(id).Entity.ToViewModel();
        }

        public BrandViewModel UpdateBrand(BrandEditViewModel model)
        {
            Brand brand = model.ToModel();
            return base.Update(brand).Entity.ToViewModel();
        }

    }
}
