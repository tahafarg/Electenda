using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FirstLyer.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.ChangeTracking;


namespace ELECTIENDA.Repository
{
    public class GeneralRepository<T> where T : class
    {
        private readonly FinalProjectContext Context;
        private DbSet<T> set;
        public GeneralRepository(FinalProjectContext _context)
        {
            Context = _context;
            set = Context.Set<T>();
        }
        public IQueryable<T> Get(Expression<Func<T,bool>>? filter =null, string orderBy = null, bool isAscending = false
                   , int pageIndex = 1, int pageSize = 20
           , params string[] includProps)
        {
            var Quary = set.AsQueryable();
            if (includProps != null)
            {
                foreach (string p in includProps)
                {
                    Quary = Quary.Include(p);
                }
            }
            if (filter != null)
                Quary = Quary.Where(filter);
            if (orderBy != null)
                Quary = Quary.OrderBy(orderBy, isAscending);
            int rowscount = Quary.Count();
            if (rowscount < pageSize)
                pageIndex = 1;
            if (pageIndex <= 0)
                pageIndex = 1;
            int PageNumber = (pageIndex - 1) * pageSize;
            Quary = Quary.Skip(PageNumber).Take(pageSize);
            return Quary;
        }

        public IQueryable<T> GetList()
        {
            return set.AsQueryable();
        }



        public EntityEntry<T> Add(T t) =>  set.Add(t);

        public EntityEntry<T> Update(T ent)
        {
            return set.Update(ent);

        }

        public EntityEntry<T> Remove(T e)
        {
            return set.Remove(e);

        }

        public EntityEntry<T> isAccepted(object _id )
        {

            var data = set.Find(_id);
            data.GetType().GetProperty("IsAccepted").SetValue(data, IsAccepted.Accepted);
            return this.Update(data);


        }

        public EntityEntry<T> isRejected(object _id)
        {
            var data = set.Find(_id);
            data.GetType().GetProperty("IsAccepted").SetValue(data, IsAccepted.Rejected);
            return this.Update(data);
        }

        public EntityEntry<T> isDeleted(object _id)
        {
            var data = set.Find(_id);
            data.GetType().GetProperty("IsDeleted").SetValue(data, true);
            return this.Update(data);
        }

        //public void Delete(T model)
        //{
        //    if (model.GetType().GetProperty("IsDelete").)
        //    {
        //        T _model = model;
        //        _model.GetType().GetProperty("IsDelete").SetValue(_model, true); 

        //        this.Update(_model);
        //    }

        //}

    }

}
