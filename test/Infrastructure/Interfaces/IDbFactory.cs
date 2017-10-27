using EndecoDemo.DAL.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndecoDemo.DAL.Infrastructure.Interfaces
{
    public interface IDbFactory : IDisposable
    {
        ApplicationEntities Init();
    }
}
