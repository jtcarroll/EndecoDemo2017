using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndecoDemo.DAL.DBContext
{
    public class ApplicationEntities : DbContext
    {

        public ApplicationEntities() : base(ConfigurationManager.ConnectionStrings["EndecoDemoStockDataConnection"].ConnectionString) { } //"ApplicationEntities"

        public DbSet<Member> Members { get; set; }
        public DbSet<StockHeader> StockHeaders { get; set; }
        public DbSet<StockDetail> StockDetails { get; set; }

        public virtual void Commit()
        {
            base.SaveChanges();
        }
    }
}


