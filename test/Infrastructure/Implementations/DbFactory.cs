using EndecoDemo.DAL.DBContext;
using EndecoDemo.DAL.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndecoDemo.DAL.Infrastructure.Implementations
{
    public class DbFactory : Disposable, IDbFactory
    {
        ApplicationEntities dbContext;

        public ApplicationEntities Init()
        {
            return dbContext ?? (dbContext = new ApplicationEntities());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
