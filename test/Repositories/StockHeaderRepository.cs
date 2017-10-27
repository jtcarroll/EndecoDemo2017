using EndecoDemo.DAL.Infrastructure.Implementations;
using EndecoDemo.DAL.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndecoDemo.DAL.Repositories
{
    public class StockHeaderRepository : RepositoryBase<StockHeader>, IStockHeaderRepository
    {
        public StockHeaderRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

        public IQueryable<StockHeader> GetStockHeadersForMember(int memberId)
        {
            var stockHeaders = this.DbContext.StockHeaders.Where(c => c.MemberId == memberId);

            return stockHeaders;
        }
    }

    public interface IStockHeaderRepository : IRepository<StockHeader>
    {
        IQueryable<StockHeader> GetStockHeadersForMember(int memberId);
    }
}
