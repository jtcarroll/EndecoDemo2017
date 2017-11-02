using EndecoDemo.DAL.Infrastructure.Implementations;
using EndecoDemo.DAL.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace EndecoDemo.DAL.Repositories
{
    public class StockDetailRepository : RepositoryBase<StockDetail>, IStockDetailRepository
    {
        public StockDetailRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

        public IQueryable<StockDetail> GetStockDetailsForStockHeader(int stockHeaderId)
        {
            var stockHeaders = this.DbContext.StockDetails.Where(c => c.StockHeaderId == stockHeaderId);

            return stockHeaders;
        }

        public void SaveStockDetails(IEnumerable<StockDetail> stockDetails)
        {
            DbContext.StockDetails.AddRange(stockDetails);
            DbContext.SaveChanges();
        }

    }

    public interface IStockDetailRepository : IRepository<StockDetail>
    {
        IQueryable<StockDetail> GetStockDetailsForStockHeader(int stockHeaderId);

        void SaveStockDetails(IEnumerable<StockDetail> stockDetails);
    }
}

