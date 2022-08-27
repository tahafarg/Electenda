using ELECTIENDA.ViewModel;
using FirstLyer.Model;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;


namespace ELECTIENDA.Repository
{
    public class ServicesRepository : GeneralRepository<Services>
    {
        public ServicesRepository(FinalProjectContext _context) : base(_context)
        { }
        public PaginingViewModel<List<ServicesViewModel>> Get( string name = "", float price =0.00f, 
            string orderby = "ProviderID", bool isAscending = false, 
            int pageIndex = 1, int pageSize = 20)
        {
            var filter = PredicateBuilder.New<Services>();
            var OldFilter = filter;

            if (price>0)
                filter = filter.Or(p => p.Price == price);
            if (!string.IsNullOrEmpty(name))
                filter = filter.Or(p => p.Provider.User.FirstName.Contains(name));
            
            if (OldFilter == filter)
                filter = null;

            var Query = base.Get(filter, orderby, isAscending, pageIndex, pageSize);
            var Result
                = Query.Where(i=>i.IsDeleted==false).Select(i => new ServicesViewModel
                {
                    ID = i.ID,
                    Name = i.Name,
                    Price = i.Price,
                    Description=i.Description,
                    CategoryName = i.Category.Name,
                    ProdviderName= i.Provider.User.FirstName +" "+i.Provider.User.LastName,
                    IsActive = i.IsActive,
                    StartDate=i.StartDate,
                    EndDate=i.EndDate,
                    Src=i.Src,

                });
            PaginingViewModel<List<ServicesViewModel>>
                finalResult = new PaginingViewModel<List<ServicesViewModel>>()
                {
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    Count = base.GetList().Count(),
                    Data = Result.ToList(),
                };


            return finalResult;
        }

        public IPagedList<ServicesViewModel> Search2(string name = "", int ProviderId = 0, float price = 0.00f,
            string orderby = "ID", bool isAscending = false,
            int pageIndex = 1, int pageSize = 10)
        {
            var filter = PredicateBuilder.New<Services>();
            var OldFilter = filter;

            if (price > 0)
                filter = filter.Or(p => p.Price == price);

            if (ProviderId > 0)
                filter = filter.Or(p => p.ProviderID == ProviderId);
            if (!string.IsNullOrEmpty(name))
                filter = filter.Or(p => p.Provider.User.FirstName.Contains(name));

            if (OldFilter == filter)
                filter = null;

            var query = base.Get(filter, orderby, isAscending, pageIndex, pageSize);
            var result
                = query.Where(i => i.IsDeleted == false).Select(i => new ServicesViewModel
                {

                    ID = i.ID,
                    Name = i.Name,
                    Price = i.Price,
                    Description = i.Description,
                    CategoryName = i.Category.Name,
                    ProdviderName = i.Provider.User.FirstName + " " + i.Provider.User.LastName,
                    IsActive = i.IsActive,
                    StartDate = i.StartDate,
                    EndDate = i.EndDate,
                    //Src = i.Src,
                }).ToPagedList(pageIndex, pageSize);

            return result;
        }

        public ServicesAddViewModel? GetByID(int _Id)
        {
            return base.GetList().Where(i => i.ID == _Id).Select(i => new ServicesAddViewModel
            {
                Id = i.ID,
                Name = i.Name,
                Description = i.Description,
                Price = i.Price,
                IsActive = i.IsActive,
                StartDate = i.StartDate,
                EndDate = i.EndDate,
                ProviderId = i.ProviderID,
                
                CategoryId=i.CategoryID,
                
                

                //ProdviderName=i.Provider.User.FirstName+" "+i.Provider.User.LastName,
                //CategoryName=i.Category.Name,
            }).FirstOrDefault();
            
        }

        public ServicesAddViewModel? GetByIDForBalance(int? _Id)
        {
            return base.GetList().Where(i => i.ID == _Id).Select(i => new ServicesAddViewModel
            {
                Id = i.ID,
                Name = i.Name,
                Description = i.Description,
                Price = i.Price,
                IsActive = i.IsActive,
                StartDate = i.StartDate,
                EndDate = i.EndDate,
                ProviderId = i.ProviderID,

                CategoryId = i.CategoryID,



                //ProdviderName=i.Provider.User.FirstName+" "+i.Provider.User.LastName,
                //CategoryName=i.Category.Name,
            }).FirstOrDefault();

        }

        public ServicesViewModel Add(ServicesAddViewModel model)
        {
            Services services = model.Model();
            return base.Add(services).Entity.ToViewModel();
        }

        public List<TextValueViewModel> GetDropDownValues() =>
           GetList().Select(i => new TextValueViewModel
           {
               Value = i.ID,
               Text = i.Name
           }).ToList();

        public ServicesViewModel Update(ServicesAddViewModel model)
        {
            Services services = model.Model();
            //var filter = PredicateBuilder.New<Services>();
            //filter.Or(i => i.ID == model.Id);
            //var data= base.Get(filter).FirstOrDefault();
            //data.CategoryID = model.CategoryId;
            //data.StartDate = model.StartDate;
            //data.EndDate= model.EndDate;
            //data.Price = model.Price;
            //data.Description=model.Description;
            //data.Name = model.Name;
            //data.IsActive = model.IsActive;
           
            return base.Update(services).Entity.ToViewModel();
        }

        public ServicesViewModel ServicesAccepted(int _id)
        {
            return base.isAccepted(_id).Entity.ToViewModel();
        }

        public ServicesViewModel ServicesRejected(int _id)
        {
            return base.isRejected(_id).Entity.ToViewModel();
        }

        public ServicesViewModel SoftDeleted(int id)
        {
            return base.isDeleted(id).Entity.ToViewModel();        
        }



        public PaginingViewModel<List<ServicesAPIViewModel>> GetAPI(string Name = "", float Price = 0, string orderBy = "", bool IsAscending = false, int PageIndex = 10, int PageSize = 10)
        {
            var filter = PredicateBuilder.New<Services>();
            var oldFilter = filter;
            if (!string.IsNullOrEmpty(Name))
                filter = filter.Or(i => i.Name.Contains(Name));
            if (Price > 0.0)
                filter = filter.Or(i => i.Price == Price);
            if (filter == oldFilter)
                filter = null;
            var Query = base.Get(filter, orderBy, IsAscending, PageIndex, PageIndex, null);

            var Result = Query.Select(s => new ServicesAPIViewModel
            {
                ID = s.ID,
                ProviderID = s.ProviderID,
                Name = s.Name,
                Price = s.Price,
                Src = s.Src


            });


            PaginingViewModel<List<ServicesAPIViewModel>>
                finalResult = new PaginingViewModel<List<ServicesAPIViewModel>>
                {
                    PageIndex = PageIndex,
                    PageSize = PageSize,
                    Count = base.GetList().Count(),
                    Data = Result.ToList()

                };

            return finalResult;
        }




    }
}


