using FirstLyer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstLyer.Model;

namespace ELECTIENDA.Repository
{
    public class UnitOfWork
    {
        private readonly FinalProjectContext context;
        public UnitOfWork(FinalProjectContext _context)
        {
            context = _context;
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
