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
    public class MembershipRepository : GeneralRepository<Membership>
    {
        public MembershipRepository(FinalProjectContext _dbContext) : base(_dbContext)
        { }

        public List<TextValueViewModel> GetDropDownValues()
        {
            return
            base.GetList().Where(i => i.IsDeleted == false).Select(i => new TextValueViewModel
            {
                Value = i.ID,
                Text = i.Type
            }).ToList();
        }

        public IPagedList<MemberShipViewModel> Get(string name = "",
            string orderby = "Id", bool isAscending = false,
            int pageIndex = 1, int pageSize = 20)
        {
            var filter = PredicateBuilder.New<Membership>();
            var OldFilter = filter;
            if (string.IsNullOrEmpty(name))
                filter = filter.Or(m=>m.Type.Contains(name) );

            if (OldFilter == filter)
                filter = null;

            var Query = base.Get(filter, orderby, isAscending, pageIndex, pageSize);
            var Result
                = Query.Where(i => i.IsDeleted == false).Select(i => new MemberShipViewModel
                {
                    ID = i.ID,
                    Role = i.Type,
                    DurartionTime=i.DurationOnDays,
                    Price = i.Price

                }).ToPagedList(pageIndex, pageSize);
            


            return Result;
        }


        public MemberShipViewModel Add(MemberShipEditViewModel model)
        {
            Membership membership = model.ToModel();
            return base.Add(membership).Entity.ToViewModel();
        }

        public MemberShipViewModel SoftDeleted(int id)
        {
            return base.isDeleted(id).Entity.ToViewModel();
        }

        public bool Compare(Provider provider) 
        {
            var Date= provider.User.LockoutEnd.Value;
            DateTimeOffset now = DateTimeOffset.Now;
            int Duration = (int) now.Subtract(Date).TotalDays;
            var member = base.GetList().Where(m => m.ID == provider.MembershipID).FirstOrDefault();
            if (Duration<member.DurationOnDays)
            {
                
                return true;

            }
            else
            {
                SoftDeleted(provider.ProviderID);
                return false;
            }

        }

        public MemberShipEditViewModel? GetByID(int? _Id)
        {
            return base.GetList().Where(i => i.ID == _Id).Select(i => new MemberShipEditViewModel
            {
                ID = i.ID,
                DurartionTime=i.DurationOnDays,
                Price=i.Price,
                Role=i.Type,
            }).FirstOrDefault();

        }

        public MemberShipViewModel Update(MemberShipEditViewModel model)
        {
            Membership membership = model.ToModel();
            return base.Update(membership).Entity.ToViewModel();
        }


    }
}
