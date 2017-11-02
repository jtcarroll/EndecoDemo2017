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

        public IEnumerable<StockHeader> GetStockHeadersForMember(int memberId, int fetchCount)
        {
            this.DbContext.Configuration.LazyLoadingEnabled = false;
            var stockHeaders = this.DbContext.StockHeaders.Where(c => c.MemberId == memberId)
                .Take(fetchCount)
                .OrderByDescending(x => x.Id);
            return stockHeaders.ToList();
        }

        public StockHeader GetLastStockHeaderForMember(int memberId)
        {
            this.DbContext.Configuration.LazyLoadingEnabled = true;
            var stockHeader = this.DbContext.StockHeaders
                .Where(h => h.Id == this.DbContext.StockHeaders.Max(x => x.Id))
                .FirstOrDefault();
            return stockHeader;
        }
    }

    public interface IStockHeaderRepository : IRepository<StockHeader>
    {
        IEnumerable<StockHeader> GetStockHeadersForMember(int memberId, int fetchCount);
        StockHeader GetLastStockHeaderForMember(int memberId);
    }
}
