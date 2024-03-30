using DataAccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.UnitOfWork
{
    public class UnitOfWorks : IUnitOfWorks
    {
        private readonly DaxoneDbContext _db;
        public UnitOfWorks(DaxoneDbContext db)
        {
                _db = db;
        }
        public int SaveChange()
        {
            return _db.SaveChanges();
            
        }
    }
}
